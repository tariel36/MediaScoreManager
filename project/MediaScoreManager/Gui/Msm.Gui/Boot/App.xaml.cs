using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Msm.Core.Core;
using Msm.Core.Logging.NLog;
using Msm.Core.Wpf.Wpf;
using Msm.Core.Wpf.Wpf.Contract;
using Msm.Gui.Features;
using Msm.Gui.Infrastructure;
using Msm.Gui.Infrastructure.Paths.Providers;
using Msm.Gui.Resources;
using Msm.Gui.View;
using Syncfusion.Licensing;

namespace Msm.Gui.Boot
{
    /// <summary>
    ///     Represents the entry point of the WPF application.
    /// </summary>
    internal partial class App
    {
        /// <summary>
        ///     The license key for Syncfusion components.
        /// </summary>
        private const string SfLicenseKey = "TODO_SF_LIC_KEY";

        /// <summary>
        ///     The host responsible for managing the application's dependency injection and lifecycle.
        /// </summary>
        private IHost? _host;

        /// <summary>
        ///     The logger used for logging application events and errors.
        /// </summary>
        private ILogger<App>? _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="App" /> class.
        /// </summary>
        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense(SfLicenseKey);
        }

        /// <summary>
        ///     Handles the application startup event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The <see cref="StartupEventArgs" /> containing event data.</param>
        private async void OnStartup(object? sender, StartupEventArgs args)
        {
            _host = Host.CreateDefaultBuilder(args.Args)
                .ConfigureAppConfiguration(BuildConfiguration)
                .ConfigureServices(ConfigureServices)
                .ConfigureServices(ConfigureLibraries)
                .Build();

            _logger = _host.Services.GetService<ILogger<App>>();

            _host.Services.GetService<ICoreWpfManager>()?.Initialize();

            await _host.StartAsync();
        }

        /// <summary>
        ///     Handles the application exit event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The <see cref="ExitEventArgs" /> containing event data.</param>
        private async void OnExit(object sender, ExitEventArgs args)
        {
            if (_host == null)
            {
                return;
            }

            await _host.StopAsync().ConfigureAwait(false);

            _host.Dispose();

            _host = null;
        }

        /// <summary>
        ///     Configures the application configuration.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder" /> to configure.</param>
        private void BuildConfiguration(IConfigurationBuilder builder)
        {
            _ = builder.SetBasePath(PathsProvider.Root)
                .AddJsonFile(PathsProvider.BaseConfiguration, false, true)
                .AddJsonFile(PathsProvider.DevConfiguration, true, true)
                .AddJsonFile(PathsProvider.UserConfiguration, true, true)
                .AddJsonFile(PathsProvider.LoggerConfiguration, true, true);
        }

        /// <summary>
        ///     Configures application services.
        /// </summary>
        /// <param name="context">The <see cref="HostBuilderContext" /> containing the host context.</param>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        private void ConfigureServices(HostBuilderContext context, IServiceCollection serviceCollection)
        {
            _ = serviceCollection
                .UseCore()
                .UseCoreWpf()
                .UseInfrastructure()
                .UseFeatures()
                .UseResources()
                .UseView();
        }

        /// <summary>
        ///     Configures third-party libraries.
        /// </summary>
        /// <param name="context">The <see cref="HostBuilderContext" /> containing the host context.</param>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        private void ConfigureLibraries(HostBuilderContext context, IServiceCollection serviceCollection)
        {
            _ = serviceCollection
                .UseNLogLogging(context);
        }

        /// <summary>
        ///     Handles unhandled exceptions in the WPF dispatcher.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The <see cref="DispatcherUnhandledExceptionEventArgs" /> containing event data.</param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            _logger?.LogError(args.Exception, nameof(OnDispatcherUnhandledException));
        }
    }
}
