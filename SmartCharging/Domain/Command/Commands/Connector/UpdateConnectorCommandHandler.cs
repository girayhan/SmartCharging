using AutoMapper;
using MediatR;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Exceptions;
using SmartCharging.Domain.Query.Queries;

namespace SmartCharging.Domain.Command.Commands.Connector
{
    public class UpdateConnectorCommandHandler : IRequestHandler<UpdateConnectorCommand>
    {
        private readonly IConnectorRepository connectorRepository;
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public UpdateConnectorCommandHandler(IConnectorRepository connectorRepository, IChargeStationRepository chargeStationRepository, IMapper mapper, IMediator mediator)
        {
            this.connectorRepository = connectorRepository;
            this.chargeStationRepository = chargeStationRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateConnectorCommand request, CancellationToken cancellationToken)
        {
            if (request.MaxCurrentInAmps < 0) throw new InvalidCapacityInAmpsException();

            var currentConnectorEntity = await connectorRepository.GetConnector(request.Id, request.ChargeStationId);
            if (currentConnectorEntity == null) throw new ConnectorDoesNotExistException();

            var chargeStationOfConnector = await chargeStationRepository.GetChargeStation(request.ChargeStationId);
            if (chargeStationOfConnector == null) throw new ChargeStationDoesNotExistException();

            await ValidateCapacity(request, currentConnectorEntity);

            var newConenctorEntity = this.mapper.Map<UpdateConnectorCommand, ConnectorEntity>(request);
            await this.connectorRepository.Update(currentConnectorEntity, newConenctorEntity);

            return Unit.Value;
        }

        private async Task ValidateCapacity(UpdateConnectorCommand request, ConnectorEntity currentConnectorEntity)
        {
            if (request.MaxCurrentInAmps < currentConnectorEntity.MaxCurrentInAmps) return;

            var groupDto = await mediator.Send(new GroupOfChargeStationQuery(request.ChargeStationId), CancellationToken.None).ConfigureAwait(false);

            var usedCapacity = groupDto!.ChargeStations
                 .SelectMany(cs => cs.Connectors)
                 .Sum(c => c.MaxCurrentInAmps);

            if (usedCapacity + request.MaxCurrentInAmps - currentConnectorEntity.MaxCurrentInAmps > groupDto.CapacityInAmps) throw new CapacityInAmpsExceededException();
        }
    }
}
