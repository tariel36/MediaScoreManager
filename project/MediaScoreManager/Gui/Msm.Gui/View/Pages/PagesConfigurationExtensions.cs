using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Msm.Core.Wpf.Observing;
using Msm.Gui.View.Pages.Contract;
using Msm.Gui.View.Pages.Models;
using Msm.Gui.View.Pages.Services;

namespace Msm.Gui.View.Pages
{
    /// <summary>
    ///     Provides extension methods for configuring page-related services in the dependency injection container for WPF
    ///     applications.
    /// </summary>
    internal static class PagesConfigurationExtensions
    {
        /// <summary>
        ///     Configures the page-related services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UsePages(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient()
                .AddSingleton();
        }

        /// <summary>
        ///     Registers a page and its associated view model in the dependency injection container.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <typeparam name="TPage">The type of the page.</typeparam>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UsePage<TViewModel, TPage>(this IServiceCollection serviceCollection, string? key = null, bool isMain = false)
            where TViewModel : Observable
            where TPage : Page
        {
            return serviceCollection
                .AddTransient<TViewModel>()
                .AddTransient<TPage>()
                .AddTransient(_ => PageMetaData.Create<TViewModel, TPage>(key, isMain));
        }

        /// <summary>
        ///     Adds transient services related to pages to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        /// <summary>
        ///     Adds singleton services related to pages to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection" />.</returns>
        private static IServiceCollection AddSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IPageService, PageService>();
        }
    }
}
