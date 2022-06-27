using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Domain.Query.DTOs;
using SmartCharging.Domain.Query.Queries.Group;

namespace SmartCharging.Controllers.Query
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ConnectorQueryController : Controller
    {
        private readonly IMediator mediator;

        public ConnectorQueryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "Connector")]
        public async Task<ConnectorDto?> GetConnector(int id, Guid chargeStationId)
        {
            return await mediator.Send(new SingleConnectorQuery(id, chargeStationId), CancellationToken.None).ConfigureAwait(false);
        }

        [HttpGet(Name = "AllConnectors")]
        public async Task<IEnumerable<ConnectorDto>> GetAllConnectors()
        {
            return await mediator.Send(new AllConnectorsQuery(), CancellationToken.None).ConfigureAwait(false);
        }
    }
}
