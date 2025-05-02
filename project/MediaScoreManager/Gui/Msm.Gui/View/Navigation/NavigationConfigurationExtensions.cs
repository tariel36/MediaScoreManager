using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.View.Navigation.Contract;
using Msm.Gui.View.Navigation.Services;

namespace Msm.Gui.View.Navigation
{
    /// <summary>
    ///     Provides extension methods for configuring navigation-related services in the dependency injection container for
    ///     WPF applications.
    /// </summary>
    internal static class NavigationConfigurationExtensions
    {
        /// <summary>
        ///     Configures the navigation-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseNavigation(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to navigation to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to navigation to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<INavigationService, NavigationService>();
        }
    }
}
