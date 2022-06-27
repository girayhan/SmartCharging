using MediatR;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class SingleConnectorQuery : IRequest<ConnectorDto?>
    {
        public SingleConnectorQuery(int id, Guid chargeStationId)
        {
            Id = id;
            ChargeStationId = chargeStationId;
        }

        public int Id { get; set; }
        public Guid ChargeStationId { get; set; }
    }
}
