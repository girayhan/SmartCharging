
namespace SmartCharging.Domain.Command.Repository
{
    public interface IChargeStationCommandRepository
    {
        Task CreateChargeStation(ChargeStationEntity chargeStationEntity);

        Task UpdateChargeStation(ChargeStationEntity chargeStationEntity);

        Task RemoveChargeStation(ChargeStationEntity chargeStationEntity);
    }
}
