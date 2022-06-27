using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Domain.Command.Commands.ChargeStation;

namespace SmartCharging.Controllers.Command
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChargeStationCommandController : Controller
    {
        private readonly IMediator mediator;

        public ChargeStationCommandController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreateChargeStation")]
        public async Task<Guid> CreateChargeStation(CreateChargeStationCommand createChargeStationCommand)
        {
            return await mediator.Send<Guid>(createChargeStationCommand, CancellationToken.None).ConfigureAwait(false);
        }

        [HttpPut(Name = "UpdateChargeStation")]
        public async Task<Unit> UpdateChargeStation(UpdateChargeStationCommand updateChargeStationCommand)
        {
            return await mediator.Send(updateChargeStationCommand, CancellationToken.None).ConfigureAwait(false);
        }

        [HttpDelete(Name = "RemoveChargeStation")]
        public async Task<Unit> RemoveChargeStation(RemoveChargeStationCommand removeChargeStationCommand)
        {
            return await mediator.Send(removeChargeStationCommand, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
