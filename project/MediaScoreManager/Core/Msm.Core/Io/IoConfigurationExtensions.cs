using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Io.Directories;
using Msm.Core.Io.Files;
using Msm.Core.Io.Paths;

namespace Msm.Core.Io
{
    /// <summary>
    ///     Provides extension methods for configuring IO-related services in the dependency injection container.
    /// </summary>
    public static class IoConfigurationExtensions
    {
        /// <summary>
        ///     Configures the core IO services, including directories, files, and paths, in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseCoreIo(this IServiceCollection serviceCollection)
        {
            return serviceCollection.UseCoreDirectories()
                .UseCoreFiles()
                .UseCorePaths();
        }
    }
}
