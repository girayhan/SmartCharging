using MediatR;
using SmartCharging.DataAccess.Repositories;

namespace SmartCharging.Domain.Command.Commands.Group
{
    public class RemoveGroupCommandHandler : IRequestHandler<RemoveGroupCommand>
    {
        private readonly IGroupRepository groupRepository;
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IConnectorRepository connectorRepository;

        public RemoveGroupCommandHandler(IGroupRepository groupRepository, IChargeStationRepository chargeStationRepository, IConnectorRepository connectorRepository)
        {
            this.groupRepository = groupRepository;
            this.chargeStationRepository = chargeStationRepository;
            this.connectorRepository = connectorRepository;
        }

        public async Task<Unit> Handle(RemoveGroupCommand request, CancellationToken cancellationToken)
        {
            var currentGroupEntity = await groupRepository.GetGroup(request.Id);
            if (currentGroupEntity == null) return Unit.Value;
            await this.groupRepository.Remove(currentGroupEntity);

            var chargeStationsOfGroup = chargeStationRepository.GetAllChargeStations().Where(cs => cs.GroupId == request.Id);
            foreach(var chargeStationEntity in chargeStationsOfGroup)
            {
                await this.chargeStationRepository.Remove(chargeStationEntity);

                var connectorsOfChargeStation = this.connectorRepository.GetAllConnectors().Where(c => c.ChargeStationId == chargeStationEntity.Id);
                foreach (var connectorEntity in connectorsOfChargeStation)
                {
                    await this.connectorRepository.Remove(connectorEntity);
                }
            }
            
            return Unit.Value;
        }
    }
}
