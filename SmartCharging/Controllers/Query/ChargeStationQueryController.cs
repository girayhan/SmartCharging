using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Domain.Query.DTOs;
using SmartCharging.Domain.Query.Queries.Group;

namespace SmartCharging.Controllers.Query
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChargeStationQueryController : Controller
    {
        private readonly IMediator mediator;

        public ChargeStationQueryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "ChargeStation")]
        public async Task<ChargeStationDto?> GetChargeStation(Guid id)
        {
            return await mediator.Send(new SingleChargeStationQuery(id), CancellationToken.None).ConfigureAwait(false);
        }

        [HttpGet(Name = "AllChargeStation")]
        public async Task<IEnumerable<ChargeStationDto>> GetAllChargeStation()
        {
            return await mediator.Send(new AllChargeStationsQuery(), CancellationToken.None).ConfigureAwait(false);
        }
    }
}
