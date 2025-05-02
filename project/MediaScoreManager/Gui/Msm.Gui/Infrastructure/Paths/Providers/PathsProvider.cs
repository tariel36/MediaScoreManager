using System.IO;
using System.Reflection;
using Msm.Core.Extensions;
using Msm.Gui.Infrastructure.Paths.Contract;

namespace Msm.Gui.Infrastructure.Paths.Providers
{
    /// <summary>
    ///     Provides paths for configuration files and the root directory of the application.
    /// </summary>
    internal class PathsProvider
        : IPathsProvider
    {
        /// <summary>
        ///     The relative path to the base configuration file.
        /// </summary>
        public const string BaseConfiguration = "Assets/Settings/appsettings.json";

        /// <summary>
        ///     The relative path to the development configuration file.
        /// </summary>
        public const string DevConfiguration = "Assets/Settings/appsettings.dev.json";

        /// <summary>
        ///     The relative path to the user configuration file.
        /// </summary>
        public const string UserConfiguration = "Assets/Settings/appsettings.user.json";

        /// <summary>
        ///     The relative path to the logger configuration file.
        /// </summary>
        public const string LoggerConfiguration = "Assets/Settings/appsettings.logger.json";

        /// <summary>
        ///     The root directory of the application, determined from the entry assembly's location.
        /// </summary>
        public static readonly string Root = StringExtensions.ValueOrEmpty(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location));

        /// <inheritdoc />
        public string RootDirectory
        {
            get { return Root; }
        }

        /// <inheritdoc />
        public string BaseConfigurationFilePath
        {
            get { return BaseConfiguration; }
        }

        /// <inheritdoc />
        public string DevConfigurationFilePath
        {
            get { return DevConfiguration; }
        }

        /// <inheritdoc />
        public string UserConfigurationFilePath
        {
            get { return UserConfiguration; }
        }

        /// <inheritdoc />
        public string LoggerConfigurationFilePath
        {
            get { return LoggerConfiguration; }
        }
    }
}
