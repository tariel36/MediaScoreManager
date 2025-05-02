using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.Features.ApplicationFeature;
using Msm.Gui.Features.InternationalizationFeature;
using Msm.Gui.Features.MainFeature;
using Msm.Gui.Features.RightPaneFeature;
using Msm.Gui.Features.ShellFeature;

namespace Msm.Gui.Features
{
    /// <summary>
    ///     Provides extension methods for configuring application features in the dependency injection container for WPF
    ///     applications.
    /// </summary>
    internal static class FeaturesConfigurationExtensions
    {
        /// <summary>
        ///     Configures all application features in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseFeatures(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .UseFeatureApplication()
                .UseFeatureShell()
                .UseFeatureRightPane()
                .UseFeatureMain()
                .UseFeatureInternationalization();
        }
    }
}
