using System.Windows;
using System.Windows.Controls;

namespace Msm.Gui.View.Frames.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Frame"/> objects in WPF.
    /// </summary>
    internal static class FrameExtensions
    {
        /// <summary>
        /// Retrieves the data context of the content within the specified <see cref="Frame"/>.
        /// </summary>
        /// <param name="frame">The <see cref="Frame"/> to retrieve the data context from.</param>
        /// <returns>The data context of the content, or <see langword="null"/> if not available.</returns>
        public static object? GetDataContext(this Frame frame)
        {
            return (frame.Content as FrameworkElement)?.DataContext;
        }

        /// <summary>
        /// Clears the navigation history of the specified <see cref="Frame"/>.
        /// </summary>
        /// <param name="frame">The <see cref="Frame"/> to clear the navigation history for.</param>
        public static void CleanNavigation(this Frame frame)
        {
            while (frame.CanGoBack)
            {
                _ = frame.RemoveBackEntry();
            }
        }
    }
}
