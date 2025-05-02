using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.View.Windows.Contract;
using Msm.Gui.View.Windows.Services;

namespace Msm.Gui.View.Windows
{
    /// <summary>
    ///     Provides extension methods for configuring window-related services in the dependency injection container for WPF
    ///     applications.
    /// </summary>
    internal static class WindowsConfigurationExtensions
    {
        /// <summary>
        ///     Configures the window management services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        internal static IServiceCollection UseWindows(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to window management to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to window management to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IWindowManagerService, WindowManagerService>();
        }
    }
}
