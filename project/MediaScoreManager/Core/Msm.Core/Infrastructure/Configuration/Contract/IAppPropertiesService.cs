using System.Runtime.CompilerServices;

namespace Msm.Core.Infrastructure.Configuration.Contract
{
    /// <summary>
    /// Defines a contract for managing application properties, including storing, loading, and retrieving values.
    /// </summary>
    public interface IAppPropertiesService
    {
        /// <summary>
        /// Stores the current application properties synchronously.
        /// </summary>
        void Store();

        /// <summary>
        /// Loads the application properties synchronously.
        /// </summary>
        void Load();

        /// <summary>
        /// Stores the current application properties asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task StoreAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the application properties asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task LoadAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets a value for the specified key in the application properties.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="key">The key for the value. Automatically provided by the caller's member name if not specified.</param>
        /// <returns>The current instance of <see cref="IAppPropertiesService"/> for method chaining.</returns>
        IAppPropertiesService Set(object? value, [CallerMemberName] string? key = null);

        /// <summary>
        /// Retrieves a value for the specified key from the application properties.
        /// </summary>
        /// <param name="key">The key for the value. Automatically provided by the caller's member name if not specified.</param>
        /// <returns>The value associated with the specified key, or null object if not found.</returns>
        object Get([CallerMemberName] string? key = null);

        /// <summary>
        /// Retrieves a value of the specified type for the specified key from the application properties.
        /// </summary>
        /// <typeparam name="TType">The type of the value to retrieve.</typeparam>
        /// <param name="key">The key for the value. Automatically provided by the caller's member name if not specified.</param>
        /// <returns>The value of the specified type associated with the key, or null object if not found.</returns>
        TType Get<TType>([CallerMemberName] string? key = null);
    }
}
