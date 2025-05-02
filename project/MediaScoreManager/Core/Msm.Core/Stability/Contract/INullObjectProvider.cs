namespace Msm.Core.Stability.Contract
{
    /// <summary>
    /// Defines a contract for managing null object instances.
    /// </summary>
    public interface INullObjectProvider
    {
        /// <summary>
        /// Registers a null object instance for the specified type.
        /// </summary>
        /// <typeparam name="TValue">The type of the null object instance.</typeparam>
        /// <param name="nullObjectInstance">The null object instance to register.</param>
        void Register<TValue>(TValue nullObjectInstance)
            where TValue : class;

        /// <summary>
        /// Resolves the registered null object instance for the specified type.
        /// </summary>
        /// <typeparam name="TValue">The type of the null object instance to resolve.</typeparam>
        /// <returns>The registered null object instance for the specified type.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if no null object instance is registered for the specified type.</exception>
        TValue Resolve<TValue>();
    }
}
