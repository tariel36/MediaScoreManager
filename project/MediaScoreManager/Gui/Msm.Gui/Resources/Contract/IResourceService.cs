using System.Windows;
using System.Windows.Media;

namespace Msm.Gui.Resources.Contract
{
    /// <summary>
    /// Defines a contract for retrieving WPF resources such as styles.
    /// </summary>
    internal interface IResourceService
    {
        /// <summary>
        /// Retrieves a style resource by its key.
        /// </summary>
        /// <param name="resourceKey">The key of the style resource to retrieve.</param>
        /// <returns>The <see cref="Style"/> associated with the specified key, or <see langword="null"/> if not found.</returns>
        Style? GetStyle(string resourceKey);

        /// <summary>
        /// Retrieves a font family resource by its key.
        /// </summary>
        /// <param name="resourceKey">The key of the font family resource to retrieve.</param>
        /// <returns>The <see cref="FontFamily"/> associated with the specified key, or <see langword="null"/> if not found.</returns>
        FontFamily? GetFontFamily(string resourceKey);
    }
}
