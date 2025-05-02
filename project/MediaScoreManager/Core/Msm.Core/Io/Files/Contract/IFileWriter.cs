namespace Msm.Core.Io.Files.Contract
{
    /// <summary>
    /// Defines a contract for writing data to files.
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        /// Writes the contents of the specified stream to a file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="stream">The stream containing the data to write.</param>
        void Write(string filePath, Stream stream);

        /// <summary>
        /// Asynchronously writes the contents of the specified stream to a file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="stream">The stream containing the data to write.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteAsync(string filePath, Stream stream, CancellationToken cancellationToken = default);
    }
}
