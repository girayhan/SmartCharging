using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Query
{
    public interface ConnectorQueryRepository
    {
        Task<ConnectorEntity> GetConnector(Guid id);

        Task<IQueryable<ConnectorEntity>> GetAllConnectors();
    }
}
