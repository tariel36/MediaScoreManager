using System.Globalization;
using System.Linq.Expressions;
using System.Resources;
using Msm.Core.Extensions;
using Msm.Core.Internationalization.Contract;

namespace Msm.Core.Internationalization.Services
{
    /// <summary>
    /// Provides basic translation logic.
    /// </summary>
    /// <param name="translationsProvider">Translations provider.</param>
    public class BaseTranslationService(ITranslationsProvider translationsProvider)
        : ITranslationService
    {
        /// <summary>
        /// The <see cref="ResourceManager"/> used to retrieve translation resources.
        /// </summary>
        private readonly ResourceManager _resourceManager = translationsProvider.ResourceManager;

        /// <summary>
        /// The current culture used for translations. Defaults to the culture provided by the <see cref="ITranslationsProvider"/>.
        /// </summary>
        private CultureInfo? _currentCulture = translationsProvider.Culture;

        /// <summary>
        /// Gets or sets the current culture used for translations.
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get
            {
                _currentCulture ??= translationsProvider.Culture;

                return _currentCulture.OrCallerThrow(nameof(CurrentCulture));
            }
            set { _currentCulture = value; }
        }

        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider, Dictionary<string, string> args)
        {
            return args.Aggregate(
                GetValueOrDefault(keyProvider),
                static (prev, curr) => prev.Replace($"{{{curr.Key}}}", curr.Value));
        }

        // TODO Use IEnumerable<TElement> instead of params in .NET 9
        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider, params object? [] args)
        {
            return string.Format(GetValueOrDefault(keyProvider), args);
        }

        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider)
        {
            string key = keyProvider.GetPropertyName();

            return _resourceManager.GetString(key, _currentCulture).Or(() => CreateFallbackValue(key));
        }

        /// <summary>
        /// Creates a fallback value for a missing translation key.
        /// </summary>
        /// <param name="key">The missing translation key.</param>
        /// <returns>A fallback string indicating the missing key.</returns>
        public static string CreateFallbackValue(string key)
        {
            return $"KEY:{key}";
        }
    }
}
