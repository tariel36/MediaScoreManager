using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Infrastructure.Configuration.Contract;
using Msm.Gui.Infrastructure.Configuration.Services;

namespace Msm.Gui.Infrastructure.Configuration
{
    /// <summary>
    ///     Provides extension methods for configuring application configuration-related services in the dependency injection
    ///     container for WPF applications.
    /// </summary>
    internal static class ConfigurationConfigurationExtensions
    {
        /// <summary>
        ///     Configures the application configuration-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseConfiguration(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to application configuration to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to application configuration to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IAppSettingsService, AppSettingsService>()
                .AddSingleton<IAppPropertiesService, AppPropertiesService>();
        }
    }
}
