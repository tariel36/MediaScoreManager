using Msm.Core.Io.Files.Contract;

namespace Msm.Core.Io.Files.Services
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IFileService"/> interface for managing file-related operations.
    /// </summary>
    public class DefaultFileService
        : IFileService
    {
        /// <inheritdoc />
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <inheritdoc />
        public Task<bool> ExistsAsync(string filePath, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() => Exists(filePath), cancellationToken);
        }
    }
}
