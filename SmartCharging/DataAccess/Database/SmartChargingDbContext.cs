using Microsoft.EntityFrameworkCore;
using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Database
{
    public class SmartChargingDbContext : DbContext, IDbContext
    {
        public SmartChargingDbContext(DbContextOptions options) : base(options) 
        {             
        }

        public DbSet<GroupEntity> Groups { get; set; }

        public DbSet<ConnectorEntity> Connectors { get; set; }

        public DbSet<ChargeStationEntity> ChargeStations { get; set; }

        public void Create<TEntity>(TEntity entity) where TEntity : class
        {
            this.Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            this.Remove(entity);
        }

        public IQueryable<TEntity> GetEntitySet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public Task<int> SaveAsync()
        {            
            return this.SaveChangesAsync();
        }

        public void Update<TEntity>(TEntity currentEntity, TEntity newEntity) where TEntity : class
        {
            this.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }
    }
}
