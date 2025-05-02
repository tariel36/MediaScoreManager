using System;
using System.Windows.Controls;

namespace Msm.Gui.Features.RightPaneFeature.RightPane.Contract
{
    /// <summary>
    /// Defines a contract for managing the right pane in a WPF application.
    /// </summary>
    internal interface IRightPaneService
    {
        /// <summary>
        /// Occurs when the right pane is opened.
        /// </summary>
        event EventHandler? PaneOpened;

        /// <summary>
        /// Occurs when the right pane is closed.
        /// </summary>
        event EventHandler? PaneClosed;

        /// <summary>
        /// Opens a page in the right pane.
        /// </summary>
        /// <param name="pageKey">The key identifying the page to open.</param>
        /// <param name="parameter">An optional parameter to pass to the page.</param>
        void OpenInRightPane(string pageKey, object? parameter = null);

        /// <summary>
        /// Initializes the right pane with the specified frame.
        /// </summary>
        /// <param name="rightPaneFrame">The frame to use for the right pane.</param>
        void Initialize(Frame rightPaneFrame);

        /// <summary>
        /// Cleans up resources and unsubscribes from events related to the right pane.
        /// </summary>
        void CleanUp();
    }
}
