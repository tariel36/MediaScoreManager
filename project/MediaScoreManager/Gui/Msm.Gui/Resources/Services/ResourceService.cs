using System.Windows;
using System.Windows.Media;
using Msm.Gui.Resources.Contract;

namespace Msm.Gui.Resources.Services
{
    /// <summary>
    ///     Provides an implementation of the <see cref="IResourceService" /> interface for retrieving WPF resources such as
    ///     styles.
    /// </summary>
    internal class ResourceService
        : IResourceService
    {
        /// <inheritdoc />
        public Style? GetStyle(string resourceKey)
        {
            return Application.Current.FindResource(resourceKey) as Style;
        }

        /// <inheritdoc />
        public FontFamily? GetFontFamily(string resourceKey)
        {
            return Application.Current.FindResource(resourceKey) as FontFamily;
        }
    }
}
