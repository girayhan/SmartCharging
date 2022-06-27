using SmartCharging.DataAccess.Database;
using SmartCharging.DataAccess.Repositories;

namespace SmartCharging.Configuration
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IChargeStationRepository, ChargeStationRepository>();
            serviceCollection.AddScoped<IGroupRepository, GroupRepository>();
            serviceCollection.AddScoped<IConnectorRepository, ConnectorRepository>();

            serviceCollection.AddScoped<IDbContext, SmartChargingDbContext>();

            return serviceCollection;
        }
    }
}
