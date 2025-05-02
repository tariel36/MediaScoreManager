using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Maintenance.Logging;

namespace Msm.Core.Maintenance
{
    /// <summary>
    ///     Provides extension methods for configuring maintenance-related services in the dependency injection container.
    /// </summary>
    public static class MaintenanceConfigurationExtensions
    {
        /// <summary>
        ///     Configures the core maintenance services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseCoreMaintenance(this IServiceCollection serviceCollection)
        {
            return serviceCollection.UseCoreLogging();
        }
    }
}
