using System.Reflection;
using System.Windows.Data;
using Msm.Core.Wpf.Internationalization.Services;

namespace Msm.Core.Wpf.XamlExtensions
{
    /// <summary>
    /// Provides a custom binding extension for internationalization (i18n) in WPF applications.
    /// </summary>
    public class I18NExtension
        : Binding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="I18NExtension"/> class using a property key and data context.
        /// </summary>
        /// <param name="propertyKey">The key of the property to bind to.</param>
        /// <param name="dataContext">The data context containing the property.</param>
        public I18NExtension(string propertyKey, object dataContext)
            : base(CreateKey(ExtractKey(propertyKey, dataContext)))
        {
            Key = ExtractKey(propertyKey, dataContext);
            Provider = InternalTranslationService.Instance;

            Mode = BindingMode.OneWay;
            Source = Provider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="I18NExtension"/> class using a key.
        /// </summary>
        /// <param name="key">The key to bind to.</param>
        public I18NExtension(string key)
            : base(CreateKey(key))
        {
            Key = key;
            Provider = InternalTranslationService.Instance;

            Mode = BindingMode.OneWay;
            Source = Provider;
        }

        /// <summary>
        /// Gets the key used for the binding.
        /// </summary>
        private string Key { get; }

        /// <summary>
        /// Gets the translation provider used for retrieving translations.
        /// </summary>
        private InternalTranslationService Provider { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return Provider.GetValueOrDefault(Key);
        }

        /// <summary>
        /// Creates a formatted key for the binding.
        /// </summary>
        /// <param name="key">The key to format.</param>
        /// <returns>A formatted key string.</returns>
        private static string CreateKey(string key)
        {
            return $"[{key}]";
        }

        /// <summary>
        /// Extracts the key from the specified property key and data context.
        /// </summary>
        /// <param name="propertyKey">The key of the property to extract.</param>
        /// <param name="dataContext">The data context containing the property.</param>
        /// <returns>The extracted key as a string.</returns>
        private static string ExtractKey(string propertyKey, object dataContext)
        {
            return dataContext.GetType()
                       .GetProperty(propertyKey, BindingFlags.Public | BindingFlags.Instance)
                       ?.GetValue(dataContext)
                       ?.ToString()
                   ?? propertyKey;
        }
    }
}
