using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Text.Encoding.Contract;
using Msm.Core.Text.Encoding.Providers;

namespace Msm.Core.Text.Encoding
{
    /// <summary>
    ///     Provides extension methods for configuring encoding-related services in the dependency injection container.
    /// </summary>
    public static class EncodingConfigurationExtensions
    {
        /// <summary>
        ///     Configures the core encoding services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseCoreEncoding(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to encoding to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to encoding to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IDefaultEncodingProvider, DefaultEncodingProvider>();
        }
    }
}
