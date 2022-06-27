using MediatR;
using SmartCharging.DataAccess.Repositories;

namespace SmartCharging.Domain.Command.Commands.ChargeStation
{
    public class RemoveChargeStationCommandHandler : IRequestHandler<RemoveChargeStationCommand>
    {
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IConnectorRepository connectorRepository;

        public RemoveChargeStationCommandHandler(IChargeStationRepository chargeStationRepository, IConnectorRepository connectorRepository)
        {
            this.chargeStationRepository = chargeStationRepository;
            this.connectorRepository = connectorRepository;
        }

        public async Task<Unit> Handle(RemoveChargeStationCommand request, CancellationToken cancellationToken)
        {
            var chargeStationEntity = await chargeStationRepository.GetChargeStation(request.Id);
            if (chargeStationEntity == null) return Unit.Value;

            await chargeStationRepository.Remove(chargeStationEntity);

            var connectorsOfChargeStation = connectorRepository.GetAllConnectors().Where(c => c.ChargeStationId == request.Id);
            foreach (var connectorEntity in connectorsOfChargeStation)
            {
                await connectorRepository.Remove(connectorEntity);
            }

            return Unit.Value;
        }
    }
}
