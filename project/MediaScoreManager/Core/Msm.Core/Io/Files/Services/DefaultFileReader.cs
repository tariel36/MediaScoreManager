using Msm.Core.Io.Files.Contract;

namespace Msm.Core.Io.Files.Services
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IFileReader"/> interface for reading files.
    /// </summary>
    public class DefaultFileReader
        : IFileReader
    {
        /// <inheritdoc />
        public Stream Open(string filePath)
        {
            return File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <inheritdoc />
        public Task<Stream> OpenAsync(string filePath, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() => Open(filePath), cancellationToken);
        }
    }
}
