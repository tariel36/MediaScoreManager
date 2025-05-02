using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.Features.ApplicationFeature.Application.Services;

namespace Msm.Gui.Features.ApplicationFeature
{
    /// <summary>
    ///     Provides extension methods for configuring the application feature in the dependency injection container for WPF
    ///     applications.
    /// </summary>
    internal static class ApplicationFeatureConfigurationExtensions
    {
        /// <summary>
        ///     Configures the application feature-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseFeatureApplication(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton()
                .AddHostedService();
        }

        /// <summary>
        ///     Adds transient services related to the application feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to the application feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds hosted services related to the application feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddHostedService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddHostedService<ApplicationHostService>();
        }
    }
}
