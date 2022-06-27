using MediatR;

namespace SmartCharging.Domain.Command.Commands.Connector
{
    public class RemoveConnectorCommand : IRequest
    {
        public int Id { get; set; }

        public Guid ChargeStationId { get; set; }
    }
}
