using Microsoft.EntityFrameworkCore;
using SmartCharging.DataAccess.Database;
using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Repositories
{
    public class ConnectorRepository : IConnectorRepository
    {
        private readonly IDbContext dbContext;

        public ConnectorRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(ConnectorEntity connectorEntity)
        {
            dbContext.Create(connectorEntity);
            await dbContext.SaveAsync();
        }

        public IQueryable<ConnectorEntity> GetAllConnectors()
        {
            return dbContext.GetEntitySet<ConnectorEntity>().AsQueryable();
        }

        public async Task<ConnectorEntity?> GetConnector(int id, Guid chargeStationId)
        {
            return await dbContext.GetEntitySet<ConnectorEntity>().FirstOrDefaultAsync(cs => cs.Id == id && cs.ChargeStationId == chargeStationId);
        }

        public async Task Remove(ConnectorEntity connectorEntity)
        {
            dbContext.Delete(connectorEntity);
            await dbContext.SaveAsync();
        }

        public async Task Update(ConnectorEntity currentConnectorEntity, ConnectorEntity newConnectorEntity)
        {
            dbContext.Update(currentConnectorEntity, newConnectorEntity);
            await dbContext.SaveAsync();
        }
    }
}
