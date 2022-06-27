using MediatR;
using System.Text.Json.Serialization;

namespace SmartCharging.Domain.Command.Commands.Group
{
    public class CreateGroupCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double CapacityInAmps { get; set; }
    }
}
