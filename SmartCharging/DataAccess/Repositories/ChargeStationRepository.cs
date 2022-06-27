using Microsoft.EntityFrameworkCore;
using SmartCharging.DataAccess.Database;
using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Repositories
{
    public class ChargeStationRepository : IChargeStationRepository
    {
        private readonly IDbContext dbContext;

        public ChargeStationRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(ChargeStationEntity chargeStationEntity)
        {
            dbContext.Create(chargeStationEntity);
            await dbContext.SaveAsync();
        }

        public IQueryable<ChargeStationEntity> GetAllChargeStations()
        {
            return dbContext.GetEntitySet<ChargeStationEntity>().AsQueryable();
        }

        public async Task<ChargeStationEntity?> GetChargeStation(Guid id)
        {
            return await dbContext.GetEntitySet<ChargeStationEntity>().FirstOrDefaultAsync(cs => cs.Id == id);
        }

        public async Task Remove(ChargeStationEntity chargeStationEntity)
        {
            dbContext.Delete(chargeStationEntity);
            await dbContext.SaveAsync();
        }

        public async Task Update(ChargeStationEntity currentChargeStationEntity, ChargeStationEntity newChargeStationEntity)
        {
            dbContext.Update(currentChargeStationEntity, newChargeStationEntity);
            await dbContext.SaveAsync();
        }
    }
}
