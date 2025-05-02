using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.Features.MainFeature.Main.ViewModels;
using Msm.Gui.Features.MainFeature.Main.Views;
using Msm.Gui.View.Pages;

namespace Msm.Gui.Features.MainFeature
{
    /// <summary>
    ///     Provides extension methods for configuring the main feature in the dependency injection container for WPF
    ///     applications.
    /// </summary>
    internal static class MainFeatureConfigurationExtensions
    {
        /// <summary>
        ///     Configures the main feature-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseFeatureMain(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton()
                .UsePage<MainViewModel, MainPage>(nameof(MainPage), true);
        }

        /// <summary>
        ///     Adds transient services related to the main feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to the main feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}
