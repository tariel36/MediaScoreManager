using System.Globalization;
using Msm.Core.Extensions;
using Msm.Core.Serialization.Contract;
using Msm.Core.Stability.Contract;
using Msm.Core.Streaming.Models;
using Msm.Core.Text.Encoding.Contract;
using Newtonsoft.Json;

namespace Msm.Core.Serialization.Services
{
    /// <summary>
    /// Provides JSON serialization and deserialization services using Newtonsoft.Json.
    /// </summary>
    /// <param name="defaultEncodingProvider">The provider for default text encoding.</param>
    /// <param name="nullObjectProvider">The provider for resolving null object instances.</param>
    public class NewtonsoftJsonSerializer(IDefaultEncodingProvider defaultEncodingProvider, INullObjectProvider nullObjectProvider)
        : IJsonSerializer
    {
        /// <summary>
        /// The default settings used for JSON serialization and deserialization.
        /// </summary>
        public static readonly JsonSerializerSettings DefaultSettings = new()
        {
            Culture = CultureInfo.InvariantCulture,
            CheckAdditionalContent = false,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.DateTime,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            FloatFormatHandling = FloatFormatHandling.DefaultValue,
            FloatParseHandling = FloatParseHandling.Decimal,
            Formatting = Formatting.Indented,
            MetadataPropertyHandling = MetadataPropertyHandling.Default,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Auto,
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            StringEscapeHandling = StringEscapeHandling.Default,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
            TypeNameHandling = TypeNameHandling.All
        };

        /// <inheritdoc />
        public Stream SerializeToStream(object? obj)
        {
            MemoryStream memoryStream = new();

            using StreamWriter streamWriter = new(memoryStream, defaultEncodingProvider.Provide(), leaveOpen: true);

            using (JsonTextWriter jsonWriter = new(streamWriter))
            {
                JsonSerializer serializer = JsonSerializer.Create(DefaultSettings);

                serializer.Serialize(jsonWriter, obj);

                jsonWriter.Flush();
            }

            memoryStream.Position = StreamPosition.Beginning;

            return memoryStream;
        }

        /// <inheritdoc />
        public async Task<Stream> SerializeToStreamAsync(object? obj, CancellationToken cancellationToken = default)
        {
            MemoryStream memoryStream = new();

            await using StreamWriter streamWriter = new(memoryStream, defaultEncodingProvider.Provide(), leaveOpen: true);

            await using (JsonTextWriter jsonWriter = new(streamWriter))
            {
                JsonSerializer serializer = JsonSerializer.Create(DefaultSettings);

                serializer.Serialize(jsonWriter, obj);

                await jsonWriter.FlushAsync(cancellationToken).ConfigureAwait(false);
            }

            memoryStream.Position = StreamPosition.Beginning;

            return memoryStream;
        }

        /// <inheritdoc />
        public TType DeserializeFromStream<TType>(Stream stream)
        {
            using StreamReader streamReader = new(stream, defaultEncodingProvider.Provide(), leaveOpen: true);
            using JsonTextReader jsonReader = new(streamReader);

            JsonSerializer serializer = JsonSerializer.Create(DefaultSettings);

            TType? result = serializer.Deserialize<TType>(jsonReader);

            Type targetType = typeof(TType);

            if (result is null && targetType.IsClass)
            {
                return nullObjectProvider.Resolve<TType>();
            }

            return targetType.Create<TType>();
        }

        /// <inheritdoc />
        public async Task<TType> DeserializeFromStreamAsync<TType>(Stream stream, CancellationToken cancellationToken = default)
        {
            return await Task.Factory.StartNew(() => DeserializeFromStream<TType>(stream), cancellationToken).ConfigureAwait(false);
        }
    }
}
