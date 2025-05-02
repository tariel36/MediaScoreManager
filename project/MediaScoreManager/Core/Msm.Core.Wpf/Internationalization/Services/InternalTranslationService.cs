using System.Linq.Expressions;
using System.Reflection;
using Msm.Core.Extensions;
using Msm.Core.Internationalization.Contract;
using Msm.Core.Internationalization.Services;

namespace Msm.Core.Wpf.Internationalization.Services
{
    /// <summary>
    ///     Provides a singleton service for managing translations and the current culture in a WPF application.
    /// </summary>
    internal class InternalTranslationService
        : ITranslationService
    {
        // TODO Replace with Lock class in .NET 9

        /// <summary>
        /// Synchronization object used to ensure thread safety when initializing the singleton instance.
        /// </summary>
        private static readonly object SyncRoot = new();

        /// <summary>
        /// Singleton instance of the <see cref="InternalTranslationService"/>.
        /// </summary>
        private static InternalTranslationService? _instance;

        /// <summary>
        /// The underlying translation service used for retrieving localized strings.
        /// </summary>
        private readonly ITranslationService _inner;

        /// <summary>
        /// The translations provider used for managing translation resources and culture information.
        /// </summary>
        private readonly ITranslationsProvider _provider;

        /// <summary>
        /// Cache for storing member expressions associated with translation keys.
        /// </summary>
        private readonly Dictionary<string, Expression<Func<string>>> _expressionsCache = new();

        /// <summary>
        ///     Prevents instantiation from outside the class.
        /// </summary>
        private InternalTranslationService(ITranslationService inner, ITranslationsProvider provider)
        {
            _inner = inner;
            _provider = provider;
        }

        /// <summary>
        ///     Gets the singleton instance of the <see cref="InternalTranslationService" />.
        /// </summary>
        internal static InternalTranslationService Instance
        {
            get { return _instance.OrCallerThrow<TypeInitializationException, InternalTranslationService>(); }
        }

        /// <summary>
        /// Get translation based on the key.
        /// </summary>
        /// <param name="key">Key to lookup</param>
        /// <returns>Translated value.</returns>
        public string this[string key]
        {
            get { return GetValueOrDefault(key); }
        }

        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider, Dictionary<string, string> args)
        {
            return _inner.GetValueOrDefault(keyProvider, args);
        }

        // TODO Use IEnumerable<TElement> instead of params in .NET 9
        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider, params object? [] args)
        {
            return _inner.GetValueOrDefault(keyProvider, args);
        }

        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider)
        {
            return _inner.GetValueOrDefault(keyProvider);
        }

        /// <summary>
        ///     Initializes the singleton instance of the <see cref="InternalTranslationService" />.
        /// </summary>
        /// <param name="inner">The underlying translation service.</param>
        /// <param name="provider">The translations provider.</param>
        internal static void Initialize(ITranslationService inner, ITranslationsProvider provider)
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new(inner, provider);
                    }
                }
            }
        }

        /// <summary>
        ///     Retrieves a translation value for the specified key.
        /// </summary>
        /// <param name="key">The translation key.</param>
        /// <returns>The translated value, or a fallback value if the key is not found.</returns>
        internal string GetValueOrDefault(string key)
        {
            if (!CreateMemberExpression(key, out Expression<Func<string>>? expr) || expr == default)
            {
                return BaseTranslationService.CreateFallbackValue(key);
            }

            return _inner.GetValueOrDefault(expr);
        }

        /// <summary>
        ///     Creates a member expression for the specified key.
        /// </summary>
        /// <param name="key">The translation key.</param>
        /// <param name="expr">The resulting member expression.</param>
        /// <returns><see langword="true" /> if the expression was successfully created; otherwise, <see langword="false" />.</returns>
        private bool CreateMemberExpression(string key, out Expression<Func<string>>? expr)
        {
            lock (SyncRoot)
            {
                if (_expressionsCache.TryGetValue(key, out expr))
                {
                    return true;
                }

                Type translationsType = _provider.ResourceManager.GetType();

                PropertyInfo? myTransProp = translationsType.GetProperty(key, BindingFlags.Static | BindingFlags.Public);

                if (myTransProp == null)
                {
                    return false;
                }

                MemberExpression propertyAccess = Expression.Property(null, myTransProp);

                expr = Expression.Lambda<Func<string>>(propertyAccess);

                _expressionsCache[key] = expr;

                return true;
            }
        }
    }
}
