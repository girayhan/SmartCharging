using AutoMapper;
using MediatR;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Exceptions;
using SmartCharging.Domain.Query.Queries;

namespace SmartCharging.Domain.Command.Commands.Connector
{
    public class CreateConnectorCommandHandler : IRequestHandler<CreateConnectorCommand>
    {
        private readonly IConnectorRepository connectorRepository;
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public CreateConnectorCommandHandler(IConnectorRepository connectorRepository, IChargeStationRepository chargeStationRepository, IMapper mapper, IMediator mediator)
        {
            this.connectorRepository = connectorRepository;
            this.chargeStationRepository = chargeStationRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateConnectorCommand request, CancellationToken cancellationToken)
        {
            if (request.MaxCurrentInAmps < 0) throw new InvalidCapacityInAmpsException();

            if (request.Id < 1 || request.Id > 5) throw new InvalidConnectorIdException();

            var connectorEntityToCheck = await this.connectorRepository.GetConnector(request.Id, request.ChargeStationId);
            if (connectorEntityToCheck != null) throw new ConnectorWithGivenIdAlreadyAlreadyExistsException();

            var chargeStationOfConnector = await this.chargeStationRepository.GetChargeStation(request.ChargeStationId);
            if(chargeStationOfConnector == null) throw new ChargeStationDoesNotExistException();

            await ValidateCapacity(request);

            var connectorEntity = this.mapper.Map<CreateConnectorCommand, ConnectorEntity>(request);
            await this.connectorRepository.Create(connectorEntity);

            return Unit.Value;
        }

        private async Task ValidateCapacity(CreateConnectorCommand request)
        {
            var groupDto = await mediator.Send(new GroupOfChargeStationQuery(request.ChargeStationId), CancellationToken.None).ConfigureAwait(false);

            var usedCapacity = groupDto!.ChargeStations
                 .SelectMany(cs => cs.Connectors)
                 .Sum(c => c.MaxCurrentInAmps);

            if (usedCapacity + request.MaxCurrentInAmps > groupDto.CapacityInAmps) throw new CapacityInAmpsExceededException();
        }
    }
}
