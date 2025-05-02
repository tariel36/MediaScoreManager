using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Msm.Core.Extensions;
using Msm.Core.Infrastructure.Configuration.Contract;
using Msm.Core.Maintenance.Logging.Contract;
using Msm.Gui.Features.ShellFeature.Shell.Contract;
using Msm.Gui.View.Navigation.Contract;
using Msm.Gui.View.Themes.Contract;
using Msm.Gui.View.Themes.Models;
using Msm.Gui.View.Windows.Contract;

namespace Msm.Gui.Features.ApplicationFeature.Application.Services
{
    /// <summary>
    ///     Provides hosting services for the WPF application, including initialization, activation, and shutdown logic.
    /// </summary>
    /// <param name="serviceProvider">The service provider for resolving dependencies.</param>
    /// <param name="navigationService">The navigation service for managing navigation within the application.</param>
    /// <param name="appPropertiesService">The service for managing application properties.</param>
    /// <param name="windowManagerService">The service for managing application windows.</param>
    /// <param name="themeService">The service for managing application themes.</param>
    /// <param name="loggingManager">The logging manager.</param>
    internal class ApplicationHostService(
        IServiceProvider serviceProvider,
        INavigationService navigationService,
        IAppPropertiesService appPropertiesService,
        IWindowManagerService windowManagerService,
        IThemeService themeService,
        ILoggingManager loggingManager)
        : IHostedService
    {
        /// <summary>
        ///     The shell window instance.
        /// </summary>
        private IShellWindow? _shellWindow;

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await InitializeAsync(cancellationToken);
            await HandleActivationAsync(cancellationToken);
            await StartupAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await appPropertiesService.StoreAsync(cancellationToken).ConfigureAwait(false);

            loggingManager.Shutdown();
        }

        /// <summary>
        ///     Initializes the application, including loading application properties and setting the theme.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            await appPropertiesService.LoadAsync(cancellationToken);

            AppThemes theme = themeService.GetCurrentTheme();
            _ = themeService.SetTheme(theme);

            await Task.CompletedTask;
        }

        /// <summary>
        ///     Handles application startup logic.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task StartupAsync(CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        ///     Handles application activation, including initializing the shell window and navigation.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task HandleActivationAsync(CancellationToken cancellationToken = default)
        {
            IReadOnlyCollection<IShellWindow> windows = windowManagerService.GetWindows<IShellWindow>().ToList();

            if (windows.IsNullOrEmpty())
            {
                _shellWindow = serviceProvider.GetService<IShellWindow>();

                if (_shellWindow == null)
                {
                    return;
                }

                navigationService.Initialize(_shellWindow.GetNavigationFrame());
                _shellWindow.ShowWindow();

                _ = navigationService.NavigateToMainView();

                await Task.CompletedTask;
            }
        }
    }
}
