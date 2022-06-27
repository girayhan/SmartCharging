using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Command
{
    public interface ConnectorCommandRepository
    {
        Task CreateConnector(ConnectorEntity connectorEntity);

        Task UpdateConnector(ConnectorEntity connectorEntity);

        Task RemoveConnector(ConnectorEntity connectorEntity);
    }
}
