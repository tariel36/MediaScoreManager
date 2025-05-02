namespace Msm.Core.Io.Directories.Contract
{
    /// <summary>
    /// Defines a contract for directory-related operations.
    /// </summary>
    public interface IDirectoryService
    {
        /// <summary>
        /// Ensures that a directory exists at the specified path. If the directory does not exist, it is created.
        /// </summary>
        /// <param name="path">The path of the directory to check or create.</param>
        void EnsureExists(string path);

        /// <summary>
        /// Asynchronously ensures that a directory exists at the specified path. If the directory does not exist, it is created.
        /// </summary>
        /// <param name="path">The path of the directory to check or create.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task EnsureExistsAsync(string path, CancellationToken cancellationToken = default);
    }
}
