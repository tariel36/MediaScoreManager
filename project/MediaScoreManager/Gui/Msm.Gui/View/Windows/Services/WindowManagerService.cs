using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Msm.Core.Extensions;
using Msm.Core.Internationalization.Contract;
using Msm.Gui.Assets.Internationalization;
using Msm.Gui.Features.ShellFeature.ShellDialog.Contract;
using Msm.Gui.Resources.Contract;
using Msm.Gui.Resources.Models;
using Msm.Gui.View.Frames.Extensions;
using Msm.Gui.View.Navigation.Contract;
using Msm.Gui.View.Pages.Contract;
using Msm.Gui.View.Windows.Contract;
using Msm.Gui.View.Windows.Extensions;
using Syncfusion.Windows.Shared;

namespace Msm.Gui.View.Windows.Services
{
    /// <summary>
    /// Provides functionality for managing WPF windows, including opening new windows, dialogs, and retrieving existing windows.
    /// </summary>
    /// <param name="serviceProvider">The service provider for resolving dependencies.</param>
    /// <param name="pageService">The service for managing pages.</param>
    /// <param name="translationService">The service for retrieving localized strings.</param>
    /// <param name="resourceService">The service for retrieving WPF resources such as styles.</param>
    internal class WindowManagerService(
        IServiceProvider serviceProvider,
        IPageService pageService,
        ITranslationService translationService,
        IResourceService resourceService)
        : IWindowManagerService
    {
        /// <inheritdoc />
        public Window? MainWindow
        {
            get { return Application.Current.MainWindow; }
        }

        /// <inheritdoc />
        public void OpenInNewWindow(string key, object? parameter = null)
        {
            Window? window = GetWindow(key);

            if (window == null)
            {
                window = new ChromelessWindow
                {
                    Title = translationService.GetValueOrDefault(static () => Translations.MainWindowTitle),
                    Style = resourceService.GetStyle(ResourceKeys.Styles.CustomMetroWindow)
                };

                Frame frame = new()
                {
                    Focusable = false,
                    NavigationUIVisibility = NavigationUIVisibility.Hidden
                };

                window.Content = frame;

                Page? page = pageService.GetPage(key);

                window.Closed -= OnWindowClosed;
                window.Closed += OnWindowClosed;

                window.Show();

                frame.Navigated -= OnNavigated;
                frame.Navigated += OnNavigated;

                _ = frame.Navigate(page, parameter);

                return;
            }

            _ = window.Activate();
        }

        /// <inheritdoc />
        public bool? OpenInDialog(string key, object? parameter = null)
        {
            IShellDialogWindow? shellWindow = serviceProvider.GetService<IShellDialogWindow>();

            if (shellWindow is not Window wnd)
            {
                return null;
            }

            Frame frame = shellWindow.GetDialogFrame();

            frame.Navigated += OnNavigated;

            wnd.Closed += OnWindowClosed;

            Page? page = pageService.GetPage(key);

            _ = frame.Navigate(page, parameter);

            return wnd.ShowDialog();
        }

        /// <inheritdoc />
        public Window? GetWindow(string key)
        {
            foreach (Window window in GetWindows())
            {
                object? dataContext = window.GetDataContext();

                if (dataContext?.GetType().FullName == key)
                {
                    return window;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public IEnumerable<Window> GetWindows()
        {
            return Application.Current.Windows.OfType<Window>();
        }

        /// <inheritdoc />
        public IEnumerable<TWindow> GetWindows<TWindow>()
        {
            return GetWindows().OfType<TWindow>();
        }

        /// <summary>
        /// Handles the <see cref="Frame.Navigated"/> event to notify navigation-aware objects.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The event data.</param>
        private static void OnNavigated(object? sender, NavigationEventArgs args)
        {
            if (sender is not Frame frame)
            {
                return;
            }

            object? dataContext = frame.GetDataContext();

            if (dataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(args.ExtraData);
            }
        }

        /// <summary>
        /// Handles the <see cref="Window.Closed"/> event to clean up resources.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The event data.</param>
        private static void OnWindowClosed(object? sender, EventArgs args)
        {
            if (sender is not Window window)
            {
                return;
            }

            if (window.Content is Frame frame)
            {
                frame.Navigated -= OnNavigated;
            }

            window.Closed -= OnWindowClosed;
        }
    }
}
