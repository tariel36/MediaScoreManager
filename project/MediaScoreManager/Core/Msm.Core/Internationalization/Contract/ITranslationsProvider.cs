using System.Globalization;
using System.Resources;

namespace Msm.Core.Internationalization.Contract
{
    /// <summary>
    /// Defines a contract for providing translation resources and culture information.
    /// </summary>
    public interface ITranslationsProvider
    {
        /// <summary>
        /// Gets the <see cref="ResourceManager"/> used to retrieve translation resources.
        /// </summary>
        ResourceManager ResourceManager { get; }

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> representing the current culture for translations.
        /// </summary>
        CultureInfo Culture { get; }
    }
}
