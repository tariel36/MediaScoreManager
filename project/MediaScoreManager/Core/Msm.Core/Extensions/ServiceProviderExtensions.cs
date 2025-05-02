// TODO Replace with new extensions in .NET 9

using Msm.Core.Stability.Providers;

namespace Msm.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with <see cref="IServiceProvider"/>.
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Retrieves a service of the specified type from the <see cref="IServiceProvider"/>.
        /// </summary>
        /// <typeparam name="TType">The type of the service to retrieve.</typeparam>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve the service from.</param>
        /// <returns>
        /// An instance of the specified service type if found; otherwise, <see langword="default" />.
        /// </returns>
        public static TType? GetService<TType>(this IServiceProvider serviceProvider)
            where TType : class
        {
            return serviceProvider.GetService(typeof(TType)) as TType ?? NullObjectProvider.Instance.Resolve<TType>();
        }
    }
}
