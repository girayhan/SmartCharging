using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Query
{
    public interface ChargeStationQueryRepository
    {
        Task<ChargeStationEntity> GetChargeStation(Guid id);

        Task<IQueryable<ChargeStationEntity>> GetAllChargeStations();
    }
}
