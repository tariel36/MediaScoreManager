using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Serialization.Contract;
using Msm.Core.Serialization.Services;
using Newtonsoft.Json;

namespace Msm.Core.Serialization
{
    /// <summary>
    ///     Provides extension methods for configuring serialization-related services in the dependency injection container.
    /// </summary>
    public static class SerializationConfigurationExtensions
    {
        /// <summary>
        ///     Configures the core serialization services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseCoreSerialization(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton()
                .Configure();
        }

        /// <summary>
        ///     Adds transient services related to serialization to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to serialization to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
        }

        /// <summary>
        ///     Configures the default settings for Newtonsoft.Json serialization.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection Configure(this IServiceCollection serviceCollection)
        {
            JsonConvert.DefaultSettings = static () => NewtonsoftJsonSerializer.DefaultSettings;

            return serviceCollection;
        }
    }
}
