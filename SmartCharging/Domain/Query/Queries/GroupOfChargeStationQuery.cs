using MediatR;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries
{
    public class GroupOfChargeStationQuery : IRequest<GroupDto>
    {
        public GroupOfChargeStationQuery(Guid chargeStationId)
        {
            ChargeStationId = chargeStationId;
        }

        public Guid ChargeStationId { get; set; }
    }
}
