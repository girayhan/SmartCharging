using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Domain.Command.Commands.Connector;

namespace SmartCharging.Controllers.Command
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ConnectorCommandController : Controller
    {
        private readonly IMediator mediator;

        public ConnectorCommandController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreateConnector")]
        public async Task<Unit> CreateConnector(CreateConnectorCommand createConnectorCommand)
        {
            return await mediator.Send(createConnectorCommand, CancellationToken.None).ConfigureAwait(false);
        }

        [HttpPut(Name = "UpdateConnector")]
        public async Task<Unit> UpdateConnector(UpdateConnectorCommand updateConnectorCommand)
        {
            return await mediator.Send(updateConnectorCommand, CancellationToken.None).ConfigureAwait(false);
        }

        [HttpDelete(Name = "RemoveConnector")]
        public async Task<Unit> RemoveConnector(RemoveConnectorCommand removeConnectorCommand)
        {
            return await mediator.Send(removeConnectorCommand, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
