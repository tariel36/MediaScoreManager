using System.Collections.Generic;
using System.Windows;

namespace Msm.Gui.View.Windows.Contract
{
    /// <summary>
    /// Defines a contract for managing WPF windows, including opening, retrieving, and managing window instances.
    /// </summary>
    internal interface IWindowManagerService
    {
        /// <summary>
        /// Gets the main application window.
        /// </summary>
        Window? MainWindow { get; }

        /// <summary>
        /// Opens a new window with the specified key and optional parameter.
        /// </summary>
        /// <param name="key">The key identifying the window to open.</param>
        /// <param name="parameter">An optional parameter to pass to the window.</param>
        void OpenInNewWindow(string key, object? parameter = null);

        /// <summary>
        /// Opens a dialog window with the specified key and optional parameter.
        /// </summary>
        /// <param name="key">The key identifying the dialog to open.</param>
        /// <param name="parameter">An optional parameter to pass to the dialog.</param>
        /// <returns>
        /// A nullable <see cref="bool"/> indicating the dialog result:
        /// <see langword="true"/> if the dialog was accepted, <see langword="false"/> if it was canceled, or <see langword="null"/> if no result was returned.
        /// </returns>
        bool? OpenInDialog(string key, object? parameter = null);

        /// <summary>
        /// Retrieves a window instance by its key.
        /// </summary>
        /// <param name="key">The key identifying the window.</param>
        /// <returns>The <see cref="Window"/> instance if found; otherwise, <see langword="null"/>.</returns>
        Window? GetWindow(string key);

        /// <summary>
        /// Retrieves all currently open windows.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="Window"/> instances.</returns>
        IEnumerable<Window> GetWindows();

        /// <summary>
        /// Retrieves all currently open windows of the specified type.
        /// </summary>
        /// <typeparam name="TWindow">The type of windows to retrieve.</typeparam>
        /// <returns>An enumerable collection of windows of the specified type.</returns>
        IEnumerable<TWindow> GetWindows<TWindow>();
    }
}
