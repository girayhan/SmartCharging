using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Domain.Query.DTOs;
using SmartCharging.Domain.Query.Queries.Group;

namespace SmartCharging.Controllers.Query
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GroupQueryController : Controller
    {
        private readonly IMediator mediator;

        public GroupQueryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "Group")]
        public async Task<GroupDto?> GetGroup(Guid id)
        {
            return await mediator.Send(new SingleGroupQuery(id), CancellationToken.None).ConfigureAwait(false);
        }

        [HttpGet(Name = "AllGroups")]
        public async Task<IEnumerable<GroupDto>> GetAllGroups()
        {
            return await mediator.Send(new AllGroupsQuery(), CancellationToken.None).ConfigureAwait(false);
        }
    }
}
