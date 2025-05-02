using System.Globalization;
using Msm.Core.Internationalization.Services;

namespace Msm.Core.Internationalization.Mutators
{
    /// <summary>
    /// Provides methods for mutating the internal culture used by the application.
    /// </summary>
    public static class InternalCultureMutator
    {
        /// <summary>
        /// Sets the current culture for the internal translation service.
        /// </summary>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/> to set as the current culture.</param>
        public static void SetCulture(CultureInfo cultureInfo)
        {
            InternalTranslationService.Instance.CurrentCulture = cultureInfo;
        }
    }
}
