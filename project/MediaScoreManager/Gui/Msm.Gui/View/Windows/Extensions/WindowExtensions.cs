using System.Windows;
using System.Windows.Controls;
using Msm.Gui.View.Frames.Extensions;

namespace Msm.Gui.View.Windows.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Window"/> objects in WPF.
    /// </summary>
    internal static class WindowExtensions
    {
        /// <summary>
        /// Retrieves the data context of the content within the specified <see cref="Window"/>.
        /// </summary>
        /// <param name="window">The <see cref="Window"/> to retrieve the data context from.</param>
        /// <returns>The data context of the content, or <see langword="null"/> if not available.</returns>
        public static object? GetDataContext(this Window window)
        {
            return (window.Content as Frame)?.GetDataContext();
        }
    }
}
