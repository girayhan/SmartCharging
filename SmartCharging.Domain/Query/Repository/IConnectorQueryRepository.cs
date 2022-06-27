using SmartCharging.DataAccess.Entities;

namespace SmartCharging.Domain.Query.Repository
{
    public interface IConnectorQueryRepository
    {
        Task<ConnectorEntity> GetConnector(Guid id);

        Task<IQueryable<ConnectorEntity>> GetAllConnectors();
    }
}
