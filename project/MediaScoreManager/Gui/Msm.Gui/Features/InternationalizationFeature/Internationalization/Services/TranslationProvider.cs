using System.Globalization;
using System.Resources;
using Msm.Core.Internationalization.Contract;
using Msm.Gui.Assets.Internationalization;

namespace Msm.Gui.Features.InternationalizationFeature.Internationalization.Services
{
    /// <summary>
    /// Provides translation resources and culture information for the application.
    /// </summary>
    internal class TranslationProvider
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
