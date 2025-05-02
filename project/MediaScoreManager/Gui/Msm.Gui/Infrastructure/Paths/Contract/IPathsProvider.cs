namespace Msm.Gui.Infrastructure.Paths.Contract
{
    /// <summary>
    ///     Defines a contract for providing paths to configuration files and the root directory of the application.
    /// </summary>
    internal interface IPathsProvider
    {
        /// <summary>
        ///     Gets the root directory of the application.
        /// </summary>
        string RootDirectory { get; }

        /// <summary>
        ///     Gets the file path to the base configuration file.
        /// </summary>
        string BaseConfigurationFilePath { get; }

        /// <summary>
        ///     Gets the file path to the development configuration file.
        /// </summary>
        string DevConfigurationFilePath { get; }

        /// <summary>
        ///     Gets the file path to the user configuration file.
        /// </summary>
        string UserConfigurationFilePath { get; }

        /// <summary>
        ///     The relative path to the logger configuration file.
        /// </summary>
        string LoggerConfigurationFilePath { get; }
    }
}
