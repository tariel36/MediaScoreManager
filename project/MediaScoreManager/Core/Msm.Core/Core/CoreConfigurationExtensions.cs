using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Io;
using Msm.Core.Maintenance;
using Msm.Core.Serialization;
using Msm.Core.Stability;
using Msm.Core.Streaming;
using Msm.Core.Text;

namespace Msm.Core.Core
{
    /// <summary>
    /// Provides extension methods for configuring core services in the dependency injection container.
    /// </summary>
    public static class CoreConfigurationExtensions
    {
        /// <summary>
        /// Configures all core services, including IO, serialization, streaming, and text services, in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection UseCore(this IServiceCollection serviceCollection)
        {
            return serviceCollection.UseCoreMaintenance()
                .UseCoreStability()
                .UseCoreIo()
                .UseCoreSerialization()
                .UseCoreStreaming()
                .UseCoreText();
        }
    }
}
