using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.Infrastructure.Configuration;
using Msm.Gui.Infrastructure.Paths;

namespace Msm.Gui.Infrastructure
{
    /// <summary>
    /// Provides extension methods for configuring infrastructure-related services in the dependency injection container for WPF applications.
    /// </summary>
    internal static class InfrastructureConfigurationExtensions
    {
        /// <summary>
        /// Configures the infrastructure-related services, including configuration and paths, in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection UseInfrastructure(this IServiceCollection serviceCollection)
        {
            return serviceCollection.UseConfiguration()
                .UsePaths();
        }
    }
}
