using Microsoft.Extensions.Configuration;
using Msm.Core.Infrastructure.Configuration.Contract;
using Msm.Core.Infrastructure.Configuration.Services;

namespace Msm.Gui.Infrastructure.Configuration.Services
{
    /// <summary>
    /// Provides application settings specific to the GUI layer by extending the base application settings service.
    /// </summary>
    internal class AppSettingsService(IConfiguration configuration)
        : BaseAppSettingsService(configuration),
          IAppSettingsService
    {
        /// <inheritdoc />
        public string AppPropertiesDirectory
        {
            get { return GetRequiredValue(); }
        }

        /// <inheritdoc />
        public string AppPropertiesFileName
        {
            get { return GetRequiredValue(); }
        }

        /// <inheritdoc />
        public string PrivacyStatement
        {
            get { return GetRequiredValue(); }
        }
    }
}
