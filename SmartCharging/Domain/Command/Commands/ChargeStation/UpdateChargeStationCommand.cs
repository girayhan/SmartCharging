using MediatR;

namespace SmartCharging.Domain.Command.Commands.ChargeStation
{
    public class UpdateChargeStationCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid GroupId { get; set; }
    }
}
