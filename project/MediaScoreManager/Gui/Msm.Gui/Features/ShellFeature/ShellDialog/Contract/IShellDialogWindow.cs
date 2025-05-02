using System.Windows.Controls;

namespace Msm.Gui.Features.ShellFeature.ShellDialog.Contract
{
    /// <summary>
    /// Defines a contract for managing the shell dialog window in a WPF application.
    /// </summary>
    internal interface IShellDialogWindow
    {
        /// <summary>
        /// Retrieves the dialog frame used within the shell dialog window.
        /// </summary>
        /// <returns>The <see cref="Frame"/> used for dialog navigation.</returns>
        Frame GetDialogFrame();
    }
}
