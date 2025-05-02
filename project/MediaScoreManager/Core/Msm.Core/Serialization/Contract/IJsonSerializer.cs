namespace Msm.Core.Serialization.Contract
{
    /// <summary>
    /// Defines a contract for JSON serialization and deserialization operations.
    /// </summary>
    public interface IJsonSerializer
    {
        /// <summary>
        /// Serializes the specified dictionary to a stream.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A stream containing the serialized JSON data.</returns>
        Stream SerializeToStream(object? obj);

        /// <summary>
        /// Asynchronously serializes the specified dictionary to a stream.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A stream containing the serialized JSON data.</returns>
        Task<Stream> SerializeToStreamAsync(object? obj, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deserializes JSON data from a stream into an object of the specified type.
        /// </summary>
        /// <typeparam name="TType">The type of the object to deserialize into.</typeparam>
        /// <param name="stream">The stream containing the JSON data.</param>
        /// <returns>An object of the specified type.</returns>
        TType DeserializeFromStream<TType>(Stream stream);

        /// <summary>
        /// Asynchronously deserializes JSON data from a stream into an object of the specified type.
        /// </summary>
        /// <typeparam name="TType">The type of the object to deserialize into.</typeparam>
        /// <param name="stream">The stream containing the JSON data.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, containing an object of the specified type.</returns>
        Task<TType> DeserializeFromStreamAsync<TType>(Stream stream, CancellationToken cancellationToken = default);
    }
}
