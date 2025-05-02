using System.Collections;
using System.Runtime.CompilerServices;
using Msm.Core.Extensions;
using Msm.Core.Infrastructure.Configuration.Contract;
using Msm.Core.Io.Directories.Contract;
using Msm.Core.Io.Files.Contract;
using Msm.Core.Io.Paths.Contract;
using Msm.Core.Serialization.Contract;
using Msm.Core.Stability.Providers;

namespace Msm.Core.Infrastructure.Configuration.Services
{
    /// <summary>
    /// Provides a base implementation of the <see cref="IAppPropertiesService" /> interface for managing application properties.
    /// </summary>
    /// <param name="appSettingsService">The application settings service.</param>
    /// <param name="jsonSerializer">The JSON serializer for serializing and deserializing properties.</param>
    /// <param name="fileWriter">The file writer for writing serialized properties to a file.</param>
    /// <param name="fileReader">The file reader for reading serialized properties from a file.</param>
    /// <param name="directoryServices">The directory services for ensuring directories exist.</param>
    /// <param name="pathServices">The path services for combining file paths.</param>
    /// <param name="fileServices">The file services for checking file existence.</param>
    public abstract class BaseAppPropertiesService(
        IAppSettingsService appSettingsService,
        IJsonSerializer jsonSerializer,
        IFileWriter fileWriter,
        IFileReader fileReader,
        IDirectoryService directoryServices,
        IPathServices pathServices,
        IFileService fileServices)
        : IAppPropertiesService
    {
        // TODO Replace with Lock class in .NET 9
        /// <summary>
        ///     Synchronization object for thread-safe operations.
        /// </summary>
        private static readonly object SyncRoot = new();

        /// <summary>
        ///     Gets the underlying dictionary of application properties.
        /// </summary>
        protected abstract IDictionary Properties { get; }

        /// <inheritdoc />
        public void Store()
        {
            string filePath = GetPropertiesFilePath();

            directoryServices.EnsureExists(filePath);

            IDictionary copy = CopyProperties();

            using Stream stream = jsonSerializer.SerializeToStream(copy);

            fileWriter.Write(filePath, stream);
        }

        /// <inheritdoc />
        public void Load()
        {
            string filePath = GetPropertiesFilePath();

            directoryServices.EnsureExists(filePath);

            if (!fileServices.Exists(filePath))
            {
                return;
            }

            using Stream stream = fileReader.Open(filePath);

            Hashtable temp = jsonSerializer.DeserializeFromStream<Hashtable>(stream);

            SetProperties(temp);
        }

        /// <inheritdoc />
        public async Task StoreAsync(CancellationToken cancellationToken = default)
        {
            string filePath = GetPropertiesFilePath();

            await directoryServices.EnsureExistsAsync(filePath, cancellationToken).ConfigureAwait(false);

            IDictionary copy = CopyProperties();

            await using Stream stream = await jsonSerializer.SerializeToStreamAsync(copy, cancellationToken);

            await fileWriter.WriteAsync(filePath, stream, cancellationToken);
        }

        /// <inheritdoc />
        public async Task LoadAsync(CancellationToken cancellationToken = default)
        {
            string filePath = GetPropertiesFilePath();

            await directoryServices.EnsureExistsAsync(filePath, cancellationToken);

            if (!await fileServices.ExistsAsync(filePath, cancellationToken))
            {
                return;
            }

            await using Stream stream = await fileReader.OpenAsync(filePath, cancellationToken);

            Hashtable temp = await jsonSerializer.DeserializeFromStreamAsync<Hashtable>(stream, cancellationToken);

            SetProperties(temp);
        }

        /// <inheritdoc />
        public IAppPropertiesService Set(object? value, [CallerMemberName] string? key = null)
        {
            lock (SyncRoot)
            {
                Properties[key.OrCallerThrow()] = value;
            }

            return this;
        }

        /// <inheritdoc />
        public object Get([CallerMemberName] string? key = null)
        {
            lock (SyncRoot)
            {
                string actualKey = key.OrCallerThrow();

                if (Properties.Contains(actualKey))
                {
                    object? value = Properties[actualKey];

                    if (value is null)
                    {
                        return NullObjectProvider.Instance.Resolve<object>();
                    }

                    return value;
                }
            }

            return NullObjectProvider.Instance.Resolve<object>();
        }

        /// <inheritdoc />
        public TType Get<TType>([CallerMemberName] string? key = null)
        {
            object value = Get(key);

            if (value is TType actual)
            {
                return actual;
            }

            return typeof(TType).Create<TType>();
        }

        /// <summary>
        /// Gets the file path for storing application properties.
        /// </summary>
        /// <returns>The file path for the application properties file.</returns>
        private string GetPropertiesFilePath()
        {
            return pathServices.Combine(appSettingsService.AppPropertiesDirectory, appSettingsService.AppPropertiesFileName);
        }

        /// <summary>
        /// Sets the application properties from the specified dictionary.
        /// </summary>
        /// <param name="temp">The dictionary containing the properties to set.</param>
        private void SetProperties(Hashtable temp)
        {
            lock (SyncRoot)
            {
                Properties.Clear();

                temp.Keys.ForEach(
                    x =>
                    {
                        if (x is null)
                        {
                            return;
                        }

                        Properties[x] = temp[x];
                    });
            }
        }

        /// <summary>
        /// Creates a copy of the current application properties.
        /// </summary>
        /// <returns>A copy of the application properties as a dictionary.</returns>
        private Hashtable CopyProperties()
        {
            Hashtable copy;

            lock (SyncRoot)
            {
                copy = new(Properties);
            }

            return copy;
        }
    }
}
