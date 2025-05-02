using System.Globalization;
using System.Resources;
using Msm.Core.Assets.Internationalization;
using Msm.Core.Internationalization.Contract;

namespace Msm.Core.Internationalization.Services
{
    /// <summary>
    /// Provides translations for the internal translation service.
    /// </summary>
    internal class InternalTranslationProvider
        : ITranslationsProvider
    {
        /// <inheritdoc />
        public ResourceManager ResourceManager
        {
            get { return Translations.ResourceManager; }
        }

        /// <inheritdoc />
        public CultureInfo Culture
        {
            get { return Translations.Culture; }
        }
    }
}
