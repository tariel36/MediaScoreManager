using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Io.Files.Contract;
using Msm.Core.Io.Files.Services;

namespace Msm.Core.Io.Files
{
    /// <summary>
    ///     Provides extension methods for configuring file-related services in the dependency injection container.
    /// </summary>
    public static class FilesConfigurationExtensions
    {
        /// <summary>
        ///     Configures the core file services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseCoreFiles(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to files to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to files to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IFileReader, DefaultFileReader>()
                .AddSingleton<IFileWriter, DefaultFileWriter>()
                .AddSingleton<IFileService, DefaultFileService>();
        }
    }
}
