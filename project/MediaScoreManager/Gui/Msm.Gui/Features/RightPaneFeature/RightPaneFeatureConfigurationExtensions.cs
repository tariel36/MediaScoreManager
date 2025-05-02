using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.Features.RightPaneFeature.RightPane.Contract;
using Msm.Gui.Features.RightPaneFeature.RightPane.Services;

namespace Msm.Gui.Features.RightPaneFeature
{
    /// <summary>
    ///     Provides extension methods for configuring the right pane feature in the dependency injection container for WPF
    ///     applications.
    /// </summary>
    internal static class RightPaneFeatureConfigurationExtensions
    {
        /// <summary>
        ///     Configures the right pane feature-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseFeatureRightPane(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to the right pane feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to the right pane feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IRightPaneService, RightPaneService>();
        }
    }
}
