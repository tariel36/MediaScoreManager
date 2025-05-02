using Msm.Core.Internationalization.Contract;
using Msm.Core.Wpf.Internationalization.Services;
using Msm.Core.Wpf.Wpf.Contract;

namespace Msm.Core.Wpf.Wpf.Services
{
    /// <summary>
    /// Provides core WPF management functionality, including initialization of translation services.
    /// </summary>
    public class CoreWpfManager
        : ICoreWpfManager
    {
        /// <summary>
        /// The translation service used for retrieving localized strings.
        /// </summary>
        private readonly ITranslationService _translationService;

        /// <summary>
        /// The translations provider used for managing translation resources and culture information.
        /// </summary>
        private readonly ITranslationsProvider _translationsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreWpfManager"/> class.
        /// </summary>
        /// <param name="translationService">The translation service to use.</param>
        /// <param name="translationsProvider">The translations provider to use.</param>
        public CoreWpfManager(ITranslationService translationService, ITranslationsProvider translationsProvider)
        {
            _translationService = translationService;
            _translationsProvider = translationsProvider;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            InternalTranslationService.Initialize(_translationService, _translationsProvider);
        }
    }
}
