using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.Infrastructure.Configuration.Services;
using Msm.Gui.View.Themes.Contract;
using Msm.Gui.View.Themes.Services;

namespace Msm.Gui.View.Themes
{
    /// <summary>
    ///     Provides extension methods for configuring theme-related services in the dependency injection container for WPF
    ///     applications.
    /// </summary>
    internal static class ThemesConfigurationExtensions
    {
        /// <summary>
        ///     Configures the theme-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseThemes(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to themes to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to themes to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IThemeService, ThemeService>()
                .AddSingleton<IThemeAppPropertiesService, AppPropertiesService>();
        }
    }
}
