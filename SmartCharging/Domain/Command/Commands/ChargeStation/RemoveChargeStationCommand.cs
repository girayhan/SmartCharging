using MediatR;

namespace SmartCharging.Domain.Command.Commands.ChargeStation
{
    public class RemoveChargeStationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
