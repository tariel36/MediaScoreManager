using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Msm.Gui.View.Frames.Extensions;
using Msm.Gui.View.Navigation.Contract;
using Msm.Gui.View.Pages.Contract;

namespace Msm.Gui.View.Navigation.Services
{
    /// <summary>
    /// Provides functionality for managing navigation within a WPF application.
    /// </summary>
    /// <param name="pageService">The page service.</param>
    internal class NavigationService(IPageService pageService)
        : INavigationService
    {
        /// <summary>
        /// The frame used for navigation.
        /// </summary>
        private Frame? _frame;

        /// <summary>
        /// The last parameter used for navigation.
        /// </summary>
        private object? _lastParameterUsed;

        /// <inheritdoc />
        public event EventHandler<string?>? Navigated;

        /// <inheritdoc />
        public bool CanGoBack
        {
            get { return _frame?.CanGoBack == true; }
        }

        /// <inheritdoc />
        public void Initialize(Frame shellFrame)
        {
            if (_frame != null)
            {
                return;
            }

            _frame = shellFrame;

            _frame.Navigated -= OnNavigated;
            _frame.Navigated += OnNavigated;
        }

        /// <inheritdoc />
        public void UnsubscribeNavigation()
        {
            if (_frame == null)
            {
                return;
            }

            _frame.Navigated -= OnNavigated;

            _frame = null;
        }

        /// <inheritdoc />
        public void GoBack()
        {
            if (_frame == null)
            {
                return;
            }

            _frame.GoBack();
        }

        /// <inheritdoc />
        public bool NavigateTo(string? pageKey, object? parameter = null, bool clearNavigation = false)
        {
            if (_frame == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(pageKey))
            {
                return false;
            }

            Type pageType = pageService.GetPageType(pageKey);

            if (_frame.Content?.GetType() == pageType && (parameter == null || parameter.Equals(_lastParameterUsed)))
            {
                return false;
            }

            _frame.Tag = clearNavigation;

            Page? page = pageService.GetPage(pageKey);

            return NavigateToPage(page, parameter);
        }

        /// <inheritdoc />
        public bool NavigateToMainView()
        {
            return NavigateToPage(pageService.GetMainPage(), false);
        }

        /// <inheritdoc />
        public void CleanNavigation()
        {
            if (_frame == null)
            {
                return;
            }

            _frame.CleanNavigation();
        }

        /// <summary>
        /// Handles the <see cref="Frame.Navigated"/> event to notify navigation-aware objects and invoke the <see cref="Navigated"/> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The event data.</param>
        private void OnNavigated(object sender, NavigationEventArgs args)
        {
            if (sender is not Frame frame)
            {
                return;
            }

            if (frame.Tag is true)
            {
                frame.CleanNavigation();
            }

            object? dataContext = frame.GetDataContext();

            if (dataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(args.ExtraData);
            }

            Navigated?.Invoke(sender, dataContext?.GetType().FullName);
        }

        /// <summary>
        /// Navigates to provided page.
        /// </summary>
        /// <param name="page">Page to navigate to</param>
        /// <param name="parameter">Additional context.</param>
        /// <returns>True if navigation succeeded, false otherwise.</returns>
        private bool NavigateToPage(Page? page, object? parameter)
        {
            if (_frame == null)
            {
                return false;
            }

            bool navigated = _frame.Navigate(page, parameter);

            if (!navigated)
            {
                return navigated;
            }

            _lastParameterUsed = parameter;

            object? dataContext = _frame.GetDataContext();

            if (dataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedFrom();
            }

            return navigated;
        }
    }
}
