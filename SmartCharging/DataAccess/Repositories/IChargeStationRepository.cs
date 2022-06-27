using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Repositories
{
    public interface IChargeStationRepository
    {
        Task<ChargeStationEntity?> GetChargeStation(Guid id);

        IQueryable<ChargeStationEntity> GetAllChargeStations();

        Task Create(ChargeStationEntity chargeStationEntity);

        Task Update(ChargeStationEntity currentChargeStationEntity, ChargeStationEntity newChargeStationEntity);

        Task Remove(ChargeStationEntity chargeStationEntity);
    }
}
