using Microsoft.Extensions.DependencyInjection;
using Msm.Gui.Features.ShellFeature.Shell.Contract;
using Msm.Gui.Features.ShellFeature.Shell.ViewModels;
using Msm.Gui.Features.ShellFeature.Shell.Views;
using Msm.Gui.Features.ShellFeature.ShellDialog.Contract;
using Msm.Gui.Features.ShellFeature.ShellDialog.ViewModels;
using Msm.Gui.Features.ShellFeature.ShellDialog.Views;

namespace Msm.Gui.Features.ShellFeature
{
    /// <summary>
    ///     Provides extension methods for configuring the shell feature in the dependency injection container for WPF
    ///     applications.
    /// </summary>
    internal static class ShellFeatureConfigurationExtensions
    {
        /// <summary>
        ///     Configures the shell feature-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UseFeatureShell(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Adds transient services related to the shell feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IShellDialogWindow, ShellDialogWindow>()
                .AddTransient<ShellDialogViewModel>()
                .AddTransient<IShellWindow, ShellWindow>()
                .AddTransient<ShellViewModel>();
        }

        /// <summary>
        ///     Adds singleton services related to the shell feature to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}
