using SmartCharging.DataAccess.Entities;

namespace SmartCharging.Domain.Query.Repository
{
    public interface IChargeStationQueryRepository
    {
        Task<ChargeStationEntity> GetChargeStation(Guid id);

        Task<IQueryable<ChargeStationEntity>> GetAllChargeStations();
    }
}
