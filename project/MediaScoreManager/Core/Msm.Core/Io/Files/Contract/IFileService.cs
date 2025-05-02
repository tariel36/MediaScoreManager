namespace Msm.Core.Io.Files.Contract
{
    /// <summary>
    /// Defines a contract for file-related operations.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Determines whether a file exists at the specified path.
        /// </summary>
        /// <param name="filePath">The path of the file to check.</param>
        /// <returns><see langword="true" /> if the file exists; otherwise, <see langword="false" />.</returns>
        bool Exists(string filePath);

        /// <summary>
        /// Asynchronously determines whether a file exists at the specified path.
        /// </summary>
        /// <param name="filePath">The path of the file to check.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, containing <see langword="true" /> if the file exists; otherwise, <see langword="false" />.</returns>
        Task<bool> ExistsAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
