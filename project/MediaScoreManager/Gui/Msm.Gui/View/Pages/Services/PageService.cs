using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Msm.Core.Extensions;
using Msm.Core.Internationalization.Contract;
using Msm.Gui.Assets.Internationalization;
using Msm.Gui.View.Pages.Contract;
using Msm.Gui.View.Pages.Models;

namespace Msm.Gui.View.Pages.Services
{
    /// <summary>
    /// Provides functionality for managing pages and their metadata in a WPF application.
    /// </summary>
    internal class PageService
        : IPageService
    {
        // TODO Replace with Lock class in .NET 9

        /// <summary>
        /// Synchronization object for thread-safe operations.
        /// </summary>
        private static readonly object SyncRoot = new();

        /// <summary>
        /// A dictionary containing registered pages and their associated keys.
        /// </summary>
        private readonly Dictionary<string, PageMetaData> _pages;

        /// <summary>
        /// The service provider for resolving page instances.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// The translation service for retrieving localized strings.
        /// </summary>
        private readonly ITranslationService _translationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider for resolving page instances.</param>
        /// <param name="pages">A collection of page metadata to register.</param>
        /// <param name="translationService">The translation service for retrieving localized strings.</param>
        public PageService(IServiceProvider serviceProvider, IEnumerable<PageMetaData> pages, ITranslationService translationService)
        {
            _serviceProvider = serviceProvider;
            _translationService = translationService;

            _pages = new();

            pages.ForEach(RegisterPage);
        }

        /// <inheritdoc />
        public Type GetPageType(string key)
        {
            PageMetaData? metadata;

            lock (SyncRoot)
            {
                if (!_pages.TryGetValue(key, out metadata))
                {
                    throw new ArgumentException(_translationService.GetValueOrDefault(static () => Translations.PageNotFound, key));
                }
            }

            return metadata.Page;
        }

        /// <inheritdoc />
        public Page? GetPage(string key)
        {
            Type pageType = GetPageType(key);

            return _serviceProvider.GetService(pageType) as Page;
        }

        /// <inheritdoc />
        public Page? GetMainPage()
        {
            PageMetaData? metadata;

            lock (SyncRoot)
            {
                metadata = _pages
                    .Where(static x => x.Value.IsMain)
                    .GroupBy(static x => x.Value.ViewModel)
                    .Single()
                    .First()
                    .Value;
            }

            return _serviceProvider.GetService(metadata.Page) as Page;
        }

        /// <summary>
        /// Registers a page and its associated metadata.
        /// </summary>
        /// <param name="metaData">The metadata of the page to register.</param>
        private void RegisterPage(PageMetaData? metaData)
        {
            if (metaData == null)
            {
                return;
            }

            lock (SyncRoot)
            {
                string? viewModelKey = metaData.ViewModel.FullName;
                string? userKey = metaData.Key;

                if (string.IsNullOrWhiteSpace(viewModelKey))
                {
                    throw new ArgumentException(_translationService.GetValueOrDefault(static () => Translations.PageKeyIsNullOrWhiteSpace, viewModelKey));
                }

                if (_pages.ContainsKey(viewModelKey))
                {
                    throw new ArgumentException(_translationService.GetValueOrDefault(static () => Translations.TheKeyIsAlreadyConfiguredInPageService, viewModelKey));
                }

                if (!string.IsNullOrWhiteSpace(userKey) && _pages.ContainsKey(userKey))
                {
                    throw new ArgumentException(_translationService.GetValueOrDefault(static () => Translations.TheKeyIsAlreadyConfiguredInPageService, userKey));
                }

                Type type = metaData.Page;

                KeyValuePair<string, PageMetaData> existing = _pages.FirstOrDefault(x => x.Value.Page == type);

                if (!string.IsNullOrWhiteSpace(existing.Key))
                {
                    throw new ArgumentException(_translationService.GetValueOrDefault(static () => Translations.ThePageTypeIsAlreadyRegisteredWithKey, existing.Key));
                }

                _pages.Add(viewModelKey, metaData);

                if (!string.IsNullOrWhiteSpace(userKey))
                {
                    _pages.Add(userKey, metaData);
                }
            }
        }
    }
}
