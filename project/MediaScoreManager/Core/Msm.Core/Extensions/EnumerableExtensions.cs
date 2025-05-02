// TODO Replace with new extensions in .NET 9

using System.Diagnostics.CodeAnalysis;

namespace Msm.Core.Extensions
{
    /// <summary>
    ///     Provides extension methods for <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Determines whether the specified enumerable is not <see langword="null" /> and contains at least one element.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to check.</param>
        /// <returns>
        ///     <see langword="true" /> if the enumerable is not <see langword="null" /> and contains at least one element;
        ///     otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsNotNullAndEmpty<TElement>([NotNullWhen(true)] this IEnumerable<TElement?>? enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        /// <summary>
        ///     Determines whether the specified enumerable is <see langword="null" /> or contains no elements.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to check.</param>
        /// <returns>
        ///     <see langword="true" /> if the enumerable is <see langword="null" /> or contains no elements; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public static bool IsNullOrEmpty<TElement>([NotNullWhen(false)] this IEnumerable<TElement?>? enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        ///     Concatenates the elements of the enumerable into a single string, using the specified separator.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to join.</param>
        /// <param name="separator">The string to use as a separator. Defaults to <see langword="null" />.</param>
        /// <returns>A string that consists of the elements in the enumerable separated by the specified separator.</returns>
        public static string Join<TElement>(this IEnumerable<TElement>? enumerable, string? separator = null)
        {
            return string.Join(separator.OrEmpty(), enumerable.OrEmpty());
        }

        /// <summary>
        ///     Returns the enumerable if it is not <see langword="null" />, or an empty enumerable if it is
        ///     <see langword="null" />.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to check.</param>
        /// <returns>The original enumerable if it is not <see langword="null" />, or an empty enumerable otherwise.</returns>
        public static IEnumerable<TElement?> OrEmpty<TElement>(this IEnumerable<TElement?>? enumerable)
        {
            return enumerable ?? [ ];
        }

        /// <summary>
        ///     Executes the specified action for each element in the enumerable.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to iterate over.</param>
        /// <param name="predicate">The action to execute for each element.</param>
        public static void ForEach<TElement>(this IEnumerable<TElement?>? enumerable, Action<TElement?> predicate)
        {
            foreach (TElement? item in enumerable.OrEmpty())
            {
                predicate(item);
            }
        }
    }
}
