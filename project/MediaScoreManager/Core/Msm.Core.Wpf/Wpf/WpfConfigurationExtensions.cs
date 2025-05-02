using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Wpf.Internationalization;
using Msm.Core.Wpf.Wpf.Contract;
using Msm.Core.Wpf.Wpf.Services;

namespace Msm.Core.Wpf.Wpf
{
    /// <summary>
    ///     Provides extension methods for configuring core WPF services in the dependency injection container.
    /// </summary>
    public static class WpfConfigurationExtensions
    {
        /// <summary>
        ///     Configures everything from core WPF in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseCoreWpf(this IServiceCollection serviceCollection)
        {
            return serviceCollection.UseCoreWpfInternationalization()
                    .UseCoreWpfServices()
                ;
        }

        /// <summary>
        ///     Configures the core WPF services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection UseCoreWpfServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to WPF to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to WPF to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<ICoreWpfManager, CoreWpfManager>();
        }
    }
}
