using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.View.Frames;
using Msm.Gui.View.Navigation;
using Msm.Gui.View.Pages;
using Msm.Gui.View.Styles;
using Msm.Gui.View.Themes;
using Msm.Gui.View.Windows;

namespace Msm.Gui.View
{
    /// <summary>
    /// Provides extension methods for configuring view-related services in the dependency injection container for WPF applications.
    /// </summary>
    internal static class ViewConfigurationExtensions
    {
        /// <summary>
        /// Configures the view-related services, including frames, navigation, pages, styles, themes, and windows, in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection UseView(this IServiceCollection serviceCollection)
        {
            return serviceCollection.UseFrames()
                .UseNavigation()
                .UsePages()
                .UseStyles()
                .UseThemes()
                .UseWindows();
        }
    }
}
