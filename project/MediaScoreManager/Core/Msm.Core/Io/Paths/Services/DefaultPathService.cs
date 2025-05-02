using Msm.Core.Extensions;
using Msm.Core.Io.Paths.Contract;

namespace Msm.Core.Io.Paths.Services
{
    /// <summary>
    /// Provides implementation of <see cref="IPathServices"/> for path-related operations.
    /// </summary>
    public class DefaultPathService
        : IPathServices
    {
        // TODO Use IEnumerable<TElement> instead of params in .NET 9

        /// <inheritdoc />
        public string Combine(params string [] parts)
        {
            return Path.Combine(parts);
        }

        /// <inheritdoc />
        public string GetDirectory(string path)
        {
            return string.IsNullOrWhiteSpace(Path.GetExtension(path))
                ? path
                : Path.GetDirectoryName(path).OrEmpty();
        }
    }
}
