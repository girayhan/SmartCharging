using SmartCharging.DataAccess.Entities;

namespace SmartCharging.Domain.Command.Repository
{
    public interface IConnectorCommandRepository
    {
        Task CreateConnector(ConnectorEntity connectorEntity);

        Task UpdateConnector(ConnectorEntity connectorEntity);

        Task RemoveConnector(ConnectorEntity connectorEntity);
    }
}
