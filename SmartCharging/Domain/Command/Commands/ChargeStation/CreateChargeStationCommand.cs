using MediatR;
using System.Text.Json.Serialization;

namespace SmartCharging.Domain.Command.Commands.ChargeStation
{
    public class CreateChargeStationCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid GroupId { get; set; }
    }
}
