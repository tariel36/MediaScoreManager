using Msm.Core.Io.Files.Contract;

namespace Msm.Core.Io.Files.Services
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IFileWriter"/> interface for writing data to files.
    /// </summary>
    public class DefaultFileWriter
        : IFileWriter
    {
        /// <inheritdoc />
        public void Write(string filePath, Stream stream)
        {
            using FileStream fStream = Open(filePath);

            stream.CopyTo(fStream);
        }

        /// <inheritdoc />
        public async Task WriteAsync(string filePath, Stream stream, CancellationToken cancellationToken = default)
        {
            await using FileStream fStream = Open(filePath);

            await stream.CopyToAsync(fStream, cancellationToken);
        }

        /// <summary>
        /// Opens a file at the specified path for writing.
        /// </summary>
        /// <param name="filePath">The path of the file to open.</param>
        /// <returns>A <see cref="FileStream"/> for writing to the file.</returns>
        private static FileStream Open(string filePath)
        {
            return File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        }
    }
}