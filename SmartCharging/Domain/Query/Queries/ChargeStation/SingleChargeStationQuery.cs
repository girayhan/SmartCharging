using MediatR;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class SingleChargeStationQuery : IRequest<ChargeStationDto?>
    {
        public SingleChargeStationQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
