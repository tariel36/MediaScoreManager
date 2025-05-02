using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using Msm.Core.Internationalization.Contract;
using Msm.Core.Internationalization.Mutators;
using Msm.Core.Internationalization.Services;
using Msm.Core.Wpf.Observing;

namespace Msm.Gui.Features.InternationalizationFeature.Internationalization.Services
{
    /// <summary>
    /// Provides translation services for retrieving localized strings and managing the current culture in a WPF application.
    /// </summary>
    /// <param name="translationsProvider">The provider for translation resources and culture information.</param>
    internal class TranslationService(ITranslationsProvider translationsProvider)
        : Observable,
          ITranslationService
    {
        /// <summary>
        /// The base translation service used for retrieving translations.
        /// </summary>
        private readonly BaseTranslationService _baseTranslationService = new(translationsProvider);

        /// <summary>
        /// Gets or sets the current culture used for translations.
        /// </summary>
        internal CultureInfo CurrentCulture
        {
            get { return _baseTranslationService.CurrentCulture; }
            set
            {
                if (Set(() => _baseTranslationService.CurrentCulture, x => _baseTranslationService.CurrentCulture = x, value))
                {
                    InternalCultureMutator.SetCulture(CurrentCulture);
                }
            }
        }

        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider, Dictionary<string, string> args)
        {
            return _baseTranslationService.GetValueOrDefault(keyProvider, args);
        }

        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider, params object?[] args)
        {
            return _baseTranslationService.GetValueOrDefault(keyProvider, args);
        }

        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider)
        {
            return _baseTranslationService.GetValueOrDefault(keyProvider);
        }
    }
}
