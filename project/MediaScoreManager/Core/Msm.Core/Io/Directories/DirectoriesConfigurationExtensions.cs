using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Io.Directories.Contract;
using Msm.Core.Io.Directories.Services;

namespace Msm.Core.Io.Directories
{
    /// <summary>
    ///     Provides extension methods for configuring directory-related services in the dependency injection container.
    /// </summary>
    public static class DirectoriesConfigurationExtensions
    {
        /// <summary>
        ///     Configures the core directory services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseCoreDirectories(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to directories to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to directories to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IDirectoryService, DefaultDirectoryService>();
        }
    }
}
