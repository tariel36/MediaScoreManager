using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Msm.Gui.Features.RightPaneFeature.RightPane.Contract;
using Msm.Gui.View.Frames.Extensions;
using Msm.Gui.View.Navigation.Contract;
using Msm.Gui.View.Pages.Contract;

namespace Msm.Gui.Features.RightPaneFeature.RightPane.Services
{
    /// <summary>
    /// Provides functionality for managing the right pane in a WPF application.
    /// </summary>
    /// <param name="pageService">The page service.</param>
    internal class RightPaneService(IPageService pageService)
        : IRightPaneService
    {
        /// <summary>
        /// The frame used for navigation in the right pane.
        /// </summary>
        private Frame? _frame;

        /// <summary>
        /// The last parameter used for navigation.
        /// </summary>
        private object? _lastParameterUsed;

        /// <inheritdoc />
        public event EventHandler? PaneOpened;

        /// <inheritdoc />
        public event EventHandler? PaneClosed;

        /// <inheritdoc />
        public void Initialize(Frame rightPaneFrame)
        {
            _frame = rightPaneFrame;

            _frame.Navigated -= OnNavigated;
            _frame.Navigated += OnNavigated;
        }

        /// <inheritdoc />
        public void CleanUp()
        {
            if (_frame is null)
            {
                return;
            }

            _frame.Navigated -= OnNavigated;
        }

        /// <inheritdoc />
        public void OpenInRightPane(string pageKey, object? parameter = null)
        {
            Type pageType = pageService.GetPageType(pageKey);

            if (_frame?.Content?.GetType() == pageType && (parameter == null || parameter.Equals(_lastParameterUsed)))
            {
                return;
            }

            Page? page = pageService.GetPage(pageKey);

            bool? navigated = _frame?.Navigate(page, parameter);

            if (navigated != true)
            {
                return;
            }

            _lastParameterUsed = parameter;

            object? dataContext = _frame?.GetDataContext();

            if (dataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedFrom();
            }
        }

        /// <summary>
        /// Handles the <see cref="Frame.Navigated"/> event to clean navigation history and notify navigation-aware objects.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The event data.</param>
        private static void OnNavigated(object sender, NavigationEventArgs args)
        {
            if (sender is not Frame frame)
            {
                return;
            }

            frame.CleanNavigation();

            object? dataContext = frame.GetDataContext();

            if (dataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(args.ExtraData);
            }
        }

        /// <summary>
        /// Invokes the <see cref="PaneClosed"/> event when the right pane is closed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The event data.</param>
        private void OnPaneClosed(object sender, EventArgs args)
        {
            PaneClosed?.Invoke(sender, args);
        }
    }
}
