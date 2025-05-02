using System.Collections;
using System.Windows;
using Msm.Core.Infrastructure.Configuration.Contract;
using Msm.Core.Infrastructure.Configuration.Services;
using Msm.Core.Io.Directories.Contract;
using Msm.Core.Io.Files.Contract;
using Msm.Core.Io.Paths.Contract;
using Msm.Core.Serialization.Contract;
using Msm.Gui.View.Themes.Contract;

namespace Msm.Gui.Infrastructure.Configuration.Services
{
    /// <summary>
    /// Provides an implementation of <see cref="IAppPropertiesService"/> and <see cref="IThemeAppPropertiesService"/>
    /// for managing application properties and theme-related properties in a WPF application.
    /// </summary>
    /// <param name="appSettingsService">The application settings service.</param>
    /// <param name="jsonSerializer">The JSON serializer for serializing and deserializing properties.</param>
    /// <param name="fileWriter">The file writer for writing serialized properties to a file.</param>
    /// <param name="fileReader">The file reader for reading serialized properties from a file.</param>
    /// <param name="directoryServices">The directory services for ensuring directories exist.</param>
    /// <param name="pathServices">The path services for combining file paths.</param>
    /// <param name="fileServices">The file services for checking file existence.</param>
    internal class AppPropertiesService(
        IAppSettingsService appSettingsService,
        IJsonSerializer jsonSerializer,
        IFileWriter fileWriter,
        IFileReader fileReader,
        IDirectoryService directoryServices,
        IPathServices pathServices,
        IFileService fileServices)
        : BaseAppPropertiesService(appSettingsService, jsonSerializer, fileWriter, fileReader, directoryServices, pathServices, fileServices),
          IAppPropertiesService,
          IThemeAppPropertiesService
    {
        /// <inheritdoc />
        protected override IDictionary Properties
        {
            get { return Application.Current.Properties; }
        }

        /// <inheritdoc />
        public string? Theme
        {
            get { return Get<string?>(); }
            set { _ = Set(value); }
        }
    }
}
