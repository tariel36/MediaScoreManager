using Msm.Core.Io.Directories.Contract;
using Msm.Core.Io.Paths.Contract;

namespace Msm.Core.Io.Directories.Services
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IDirectoryService"/> interface for managing directories.
    /// </summary>
    /// <param name="pathServices">The service for handling path-related operations.</param>
    public class DefaultDirectoryService(IPathServices pathServices)
        : IDirectoryService
    {
        /// <inheritdoc />
        public void EnsureExists(string path)
        {
            string directoryPath = pathServices.GetDirectory(path);

            if (Directory.Exists(directoryPath))
            {
                return;
            }

            _ = Directory.CreateDirectory(directoryPath);
        }

        /// <inheritdoc />
        public Task EnsureExistsAsync(string path, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() => EnsureExists(path), cancellationToken);
        }
    }
}
