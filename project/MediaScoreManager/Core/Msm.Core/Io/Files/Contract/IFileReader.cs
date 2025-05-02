namespace Msm.Core.Io.Files.Contract
{
    /// <summary>
    /// Defines a contract for reading data from files.
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Opens a file at the specified path for reading.
        /// </summary>
        /// <param name="filePath">The path of the file to open.</param>
        /// <returns>A <see cref="Stream"/> for reading the file's contents.</returns>
        Stream Open(string filePath);

        /// <summary>
        /// Asynchronously opens a file at the specified path for reading.
        /// </summary>
        /// <param name="filePath">The path of the file to open.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, containing a <see cref="Stream"/> for reading the file's contents.</returns>
        Task<Stream> OpenAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
