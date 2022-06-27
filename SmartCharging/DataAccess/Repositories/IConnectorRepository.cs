using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Repositories
{
    public interface IConnectorRepository
    {
        Task<ConnectorEntity?> GetConnector(int id, Guid chargeStationId);

        IQueryable<ConnectorEntity> GetAllConnectors();

        Task Create(ConnectorEntity connectorEntity);

        Task Update(ConnectorEntity currentConnectorEntity, ConnectorEntity newConnectorEntity);

        Task Remove(ConnectorEntity connectorEntity);
    }
}
