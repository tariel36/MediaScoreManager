// TODO Replace with new extensions in .NET 9

using System.Runtime.CompilerServices;

namespace Msm.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with objects and selecting values.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Selects the first non-default value from the provided values or returns the fallback value if all are default.
        /// </summary>
        /// <typeparam name="TValue">The type of the values.</typeparam>
        /// <param name="fallbackValue">The fallback value to return if all values are default.</param>
        /// <param name="values">An array of values to check.</param>
        /// <returns>The first non-default value, or the fallback value if all are default.</returns>
        public static TValue SelectValue<TValue>(TValue fallbackValue, params TValue? [] values)
            where TValue : class
        {
            // TODO Use IEnumerable<TElement> instead of params in .NET 9

            foreach (TValue? value in values)
            {
                if (!Equals(value, default))
                {
                    return value;
                }
            }

            return fallbackValue;
        }

        /// <summary>
        /// Selects the first non-null value from the provided nullable value types or returns the fallback value if all are null.
        /// </summary>
        /// <typeparam name="TValue">The type of the values.</typeparam>
        /// <param name="fallbackValue">The fallback value to return if all values are null.</param>
        /// <param name="values">An array of nullable value types to check.</param>
        /// <returns>The first non-null value, or the fallback value if all are null.</returns>
        public static TValue SelectValue<TValue>(TValue fallbackValue, params TValue? [] values)
            where TValue : struct
        {
            // TODO Use IEnumerable<TElement> instead of params in .NET 9

            foreach (TValue? value in values)
            {
                if (value.HasValue)
                {
                    return value.Value;
                }
            }

            return fallbackValue;
        }

        /// <summary>
        /// Selects the provided value if it is not null, or returns the fallback value if it is null.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="fallbackValue">The fallback value to return if the value is null.</param>
        /// <param name="value">The nullable value to check.</param>
        /// <returns>The provided value if it is not null, or the fallback value otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue SelectValue<TValue>(TValue fallbackValue, TValue? value)
            where TValue : struct
        {
            return value ?? fallbackValue;
        }

        /// <summary>
        /// Determines whether the specified object is not null or its default value.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>
        /// <see langword="true" /> if the object is not null or its default value; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsNotNullOrDefault(object? obj)
        {
            if (obj is string str)
            {
                return !string.IsNullOrWhiteSpace(str);
            }

            if (obj?.GetType().IsClass == true)
            {
                return Equals(obj, null);
            }

            return Equals(obj, obj?.GetType().Create());
        }

        /// <summary>
        /// Casts the specified object to the specified type or throws an exception if the cast is invalid.
        /// </summary>
        /// <typeparam name="TType">The type to cast to.</typeparam>
        /// <param name="obj">The object to cast.</param>
        /// <returns>The object cast to the specified type.</returns>
        /// <exception cref="InvalidCastException">Thrown if the cast is invalid.</exception>
        public static TType As<TType>(this object? obj)
            where TType : class
        {
            return (obj as TType).OrCallerThrow<InvalidCastException, TType>();
        }
    }
}
