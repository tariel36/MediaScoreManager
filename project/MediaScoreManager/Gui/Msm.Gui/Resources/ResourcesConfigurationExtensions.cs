using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.Resources.Contract;
using Msm.Gui.Resources.Services;

namespace Msm.Gui.Resources
{
    /// <summary>
    ///     Provides extension methods for configuring resource-related services in the dependency injection container for WPF
    ///     applications.
    /// </summary>
    internal static class ResourcesConfigurationExtensions
    {
        /// <summary>
        ///     Configures the resource-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseResources(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to resources to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to resources to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IResourceService, ResourceService>();
        }
    }
}
