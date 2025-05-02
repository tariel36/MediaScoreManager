using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Msm.Core.Logging.NLog.Services;
using Msm.Core.Maintenance.Logging.Contract;
using NLog.Extensions.Logging;

namespace Msm.Core.Logging.NLog
{
    /// <summary>
    ///     Provides extension methods for configuring NLog logging-related services in the dependency injection container.
    /// </summary>
    public static class NLogLoggingConfigurationExtensions
    {
        /// <summary>
        ///     Configures the NLog logging services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <param name="context">The <see cref="HostBuilderContext" /> context.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseNLogLogging(this IServiceCollection serviceCollection, HostBuilderContext context)
        {
            return serviceCollection.AddTransient()
                .AddSingleton()
                .Configure(context);
        }

        /// <summary>
        ///     Adds transient services related to NLog logging to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to NLog logging to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<ILoggingManager, NLogLoggingManager>();
        }

        /// <summary>
        ///     Configures logging services using the provided <see cref="HostBuilderContext" />.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <param name="context">The <see cref="HostBuilderContext" /> containing configuration information.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection Configure(this IServiceCollection serviceCollection, HostBuilderContext context)
        {
            return serviceCollection.AddLogging(
                x => x.ClearProviders()
                    .AddNLog(context.Configuration));
        }
    }
}
