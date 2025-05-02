using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Internationalization.Contract;
using Msm.Gui.Features.InternationalizationFeature.Internationalization.Services;

namespace Msm.Gui.Features.InternationalizationFeature
{
    /// <summary>
    ///     Provides extension methods for configuring the internationalization feature in the dependency injection container
    ///     for WPF applications.
    /// </summary>
    internal static class InternationalizationFeatureConfigurationExtensions
    {
        /// <summary>
        ///     Configures the internationalization feature-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseFeatureInternationalization(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to the internationalization feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to the internationalization feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<ITranslationService, TranslationService>()
                .AddSingleton<ITranslationsProvider, TranslationProvider>();
        }
    }
}
