using MediatR;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class AllChargeStationsQuery : IRequest<IEnumerable<ChargeStationDto>>
    {
    }
}
