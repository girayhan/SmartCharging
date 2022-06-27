using MediatR;

namespace SmartCharging.Domain.Command.Commands.Group
{   
    public class UpdateGroupCommand : IRequest
    {       
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double CapacityInAmps { get; set; }
    }
}
