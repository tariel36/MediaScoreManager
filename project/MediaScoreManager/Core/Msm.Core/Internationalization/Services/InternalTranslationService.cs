using System.Globalization;
using System.Linq.Expressions;
using Msm.Core.Internationalization.Contract;

namespace Msm.Core.Internationalization.Services
{
    /// <summary>
    /// Provides a singleton service for providing translations and managing the current culture.
    /// </summary>
    internal class InternalTranslationService
        : ITranslationService
    {
        // TODO Replace with Lock class in .NET 9
        private static readonly object SyncRoot = new();

        private static InternalTranslationService? _instance;

        private readonly BaseTranslationService _baseTranslationService = new(new InternalTranslationProvider());

        /// <summary>
        /// Prevents instantiation from outside the class.
        /// </summary>
        private InternalTranslationService()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="InternalTranslationService"/>.
        /// </summary>
        internal static InternalTranslationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets or sets the current culture used for translations.
        /// </summary>
        internal CultureInfo CurrentCulture
        {
            get { return _baseTranslationService.CurrentCulture; }
            set
            {
                if (Equals(_baseTranslationService.CurrentCulture, value))
                {
                    return;
                }

                _baseTranslationService.CurrentCulture = value;
            }
        }

        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider, Dictionary<string, string> args)
        {
            return _baseTranslationService.GetValueOrDefault(keyProvider, args);
        }

        // TODO Use IEnumerable<TElement> instead of params in .NET 9
        /// <inheritdoc />
        public string GetValueOrDefault(Expression<Func<string>> keyProvider, params object? [] args)
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
