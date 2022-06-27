using MediatR;

namespace SmartCharging.Domain.Command.Commands.Connector
{
    public class UpdateConnectorCommand : IRequest
    {
        public int Id { get; set; }

        public Guid ChargeStationId { get; set; }

        public double MaxCurrentInAmps { get; set; }
    }
}
