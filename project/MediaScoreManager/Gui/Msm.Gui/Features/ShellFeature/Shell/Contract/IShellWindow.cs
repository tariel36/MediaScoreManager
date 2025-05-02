using System.Windows.Controls;

namespace Msm.Gui.Features.ShellFeature.Shell.Contract
{
    /// <summary>
    /// Defines a contract for managing the shell window in a WPF application.
    /// </summary>
    internal interface IShellWindow
    {
        /// <summary>
        /// Retrieves the navigation frame used within the shell window.
        /// </summary>
        /// <returns>The <see cref="Frame"/> used for navigation.</returns>
        Frame GetNavigationFrame();

        /// <summary>
        /// Displays the shell window.
        /// </summary>
        void ShowWindow();

        /// <summary>
        /// Closes the shell window.
        /// </summary>
        void CloseWindow();
    }
}
