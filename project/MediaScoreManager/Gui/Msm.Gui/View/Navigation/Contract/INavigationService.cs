using System;
using System.Windows.Controls;

namespace Msm.Gui.View.Navigation.Contract
{
    /// <summary>
    /// Defines a contract for managing navigation within a WPF application.
    /// </summary>
    internal interface INavigationService
    {
        /// <summary>
        /// Occurs when navigation to a new page is completed.
        /// </summary>
        event EventHandler<string?>? Navigated;

        /// <summary>
        /// Gets a value indicating whether the navigation service can navigate back to a previous page.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Initializes the navigation service with the specified frame.
        /// </summary>
        /// <param name="shellFrame">The frame to use for navigation.</param>
        void Initialize(Frame shellFrame);

        /// <summary>
        /// Navigates to the specified page.
        /// </summary>
        /// <param name="pageKey">The key identifying the page to navigate to.</param>
        /// <param name="parameter">An optional parameter to pass to the page.</param>
        /// <param name="clearNavigation">A value indicating whether to clear the navigation history.</param>
        /// <returns><see langword="true"/> if navigation was successful; otherwise, <see langword="false"/>.</returns>
        bool NavigateTo(string? pageKey, object? parameter = null, bool clearNavigation = false);

        /// <summary>
        /// Navigates to the main page.
        /// </summary>
        /// <returns><see langword="true"/> if navigation was successful; otherwise, <see langword="false"/>.</returns>
        bool NavigateToMainView();

        /// <summary>
        /// Navigates back to the previous page in the navigation history.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Unsubscribes from navigation events.
        /// </summary>
        void UnsubscribeNavigation();

        /// <summary>
        /// Clears the navigation history.
        /// </summary>
        void CleanNavigation();
    }
}
