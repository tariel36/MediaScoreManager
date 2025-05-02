using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Text.Encoding;

namespace Msm.Core.Text
{
    /// <summary>
    /// Provides extension methods for configuring text-related services in the dependency injection container.
    /// </summary>
    public static class TextConfigurationExtensions
    {
        /// <summary>
        /// Configures the core text services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection UseCoreText(this IServiceCollection serviceCollection)
        {
            return serviceCollection.UseCoreEncoding();
        }
    }
}
