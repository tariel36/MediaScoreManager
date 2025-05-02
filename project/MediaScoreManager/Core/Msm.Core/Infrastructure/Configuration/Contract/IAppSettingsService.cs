namespace Msm.Core.Infrastructure.Configuration.Contract
{
    /// <summary>
    /// Defines a contract for accessing application settings.
    /// </summary>
    public interface IAppSettingsService
    {
        /// <summary>
        /// Gets the directory path where application properties are stored.
        /// </summary>
        string AppPropertiesDirectory { get; }

        /// <summary>
        /// Gets the file name of the application properties file.
        /// </summary>
        string AppPropertiesFileName { get; }

        /// <summary>
        /// Gets the URL or path to the privacy statement.
        /// </summary>
        string PrivacyStatement { get; }
    }
}
