using MediatR;

namespace SmartCharging.Domain.Command.Commands.Group
{
    public class RemoveGroupCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
