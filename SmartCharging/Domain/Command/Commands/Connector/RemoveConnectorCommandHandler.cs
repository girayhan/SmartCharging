using MediatR;
using SmartCharging.DataAccess.Repositories;

namespace SmartCharging.Domain.Command.Commands.Connector
{
    public class RemoveConnectorCommandHandler : IRequestHandler<RemoveConnectorCommand>
    {
        private readonly IConnectorRepository connectorRepository;

        public RemoveConnectorCommandHandler(IConnectorRepository connectorRepository)
        {
            this.connectorRepository = connectorRepository;
        }

        public async Task<Unit> Handle(RemoveConnectorCommand request, CancellationToken cancellationToken)
        {
            var currentConenctorEntity = await connectorRepository.GetConnector(request.Id, request.ChargeStationId);
            if (currentConenctorEntity == null) return Unit.Value;

            await this.connectorRepository.Remove(currentConenctorEntity);

            return Unit.Value;
        }
    }
}
