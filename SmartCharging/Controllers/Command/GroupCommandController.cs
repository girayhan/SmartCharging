using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Domain.Command.Commands.Group;

namespace SmartCharging.Controllers.Command
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GroupCommandController : Controller
    {
        private readonly IMediator mediator;

        public GroupCommandController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreateGroup")]
        public async Task<Guid> CreateGroup(CreateGroupCommand createGroupCommand)
        {
            return await mediator.Send<Guid>(createGroupCommand, CancellationToken.None).ConfigureAwait(false);
        }

        [HttpPut(Name = "UpdateGroup")]
        public async Task<Unit> UpdateGroup(UpdateGroupCommand updateGroupCommand)
        {
            return await mediator.Send(updateGroupCommand, CancellationToken.None).ConfigureAwait(false);
        }

        [HttpDelete(Name = "RemoveGroup")]
        public async Task<Unit> RemoveGroup(RemoveGroupCommand removeGroupCommand)
        {
            return await mediator.Send(removeGroupCommand, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
