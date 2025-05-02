namespace Msm.Core.Io.Paths.Contract
{
    /// <summary>
    /// Defines a contract for path-related operations.
    /// </summary>
    public interface IPathServices
    {
        // TODO Use IEnumerable<TElement> instead of params in .NET 9

        /// <summary>
        /// Combines multiple path segments into a single path.
        /// </summary>
        /// <param name="parts">An array of path segments to combine.</param>
        /// <returns>A single combined path.</returns>
        string Combine(params string [] parts);

        /// <summary>
        /// Gets the directory name of a specified path. If path is already a directory, it returns the path itself.
        /// </summary>
        /// <param name="path">File or directory path</param>
        /// <returns>Path to directory.</returns>
        string GetDirectory(string path);
    }
}
