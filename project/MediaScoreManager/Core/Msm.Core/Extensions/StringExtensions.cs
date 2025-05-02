// TODO Replace with new extensions in .NET 9

namespace Msm.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the original string if it is not <see langword="null" /> or whitespace; otherwise, returns an empty string.
        /// </summary>
        /// <param name="sValue">The string to check.</param>
        /// <returns>The original string if it is not <see langword="null" /> or whitespace; otherwise, an empty string.</returns>
        public static string OrEmpty(this string? sValue)
        {
            return string.IsNullOrWhiteSpace(sValue)
                ? string.Empty
                : sValue;
        }

        /// <summary>
        /// Returns the original string if it is not <see langword="null" /> or whitespace; otherwise, returns the specified fallback string.
        /// </summary>
        /// <param name="sValue">The string to check.</param>
        /// <param name="fallback">The fallback string to return if the original string is <see langword="null" /> or whitespace.</param>
        /// <returns>The original string if it is not <see langword="null" /> or whitespace; otherwise, the fallback string.</returns>
        public static string Or(this string? sValue, string fallback)
        {
            return string.IsNullOrWhiteSpace(sValue)
                ? fallback
                : sValue;
        }

        /// <summary>
        /// Returns the original string if it is not <see langword="null" /> or whitespace; otherwise, returns the result of the specified factory function.
        /// </summary>
        /// <param name="sValue">The string to check.</param>
        /// <param name="factory">A function that produces a fallback string if the original string is <see langword="null" /> or whitespace.</param>
        /// <returns>The original string if it is not <see langword="null" /> or whitespace; otherwise, the result of the factory function.</returns>
        public static string Or(this string? sValue, Func<string> factory)
        {
            return string.IsNullOrWhiteSpace(sValue)
                ? factory()
                : sValue;
        }

        /// <summary>
        /// Selects the first non-whitespace string from the provided values or returns the fallback value if all are whitespace or <see langword="null" />.
        /// </summary>
        /// <param name="fallbackValue">The fallback value to return if all values are whitespace or <see langword="null" />.</param>
        /// <param name="values">An array of strings to check.</param>
        /// <returns>The first non-whitespace string, or the fallback value if all are whitespace or <see langword="null" />.</returns>
        public static string SelectValue(string fallbackValue, params string? [] values)
        {
            // TODO Use IEnumerable<TElement> instead of params in .NET 9

            foreach (string? value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return fallbackValue.OrEmpty();
        }

        /// <summary>
        /// Returns the original string if it is not <see langword="null" /> or whitespace; otherwise, returns an empty string.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>The original string if it is not <see langword="null" /> or whitespace; otherwise, an empty string.</returns>
        public static string ValueOrEmpty(string? value)
        {
            return value.OrEmpty();
        }
    }
}
