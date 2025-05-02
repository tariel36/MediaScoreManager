using System.Collections;

namespace Msm.Core.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Returns the collection if it is not <see langword="null" />, or an empty read-only collection if it is
        ///     <see langword="null" />.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The collection to check.</param>
        /// <returns>The original collection if it is not <see langword="null" />, or an empty read-only collection otherwise.</returns>
        public static IReadOnlyCollection<TElement?> OrEmpty<TElement>(this IReadOnlyCollection<TElement?>? collection)
        {
            return collection ?? [ ];
        }

        /// <summary>
        ///     Returns the collection if it is not <see langword="null" />, or an empty collection if it is
        ///     <see langword="null" />.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The collection to check.</param>
        /// <returns>The original collection if it is not <see langword="null" />, or an empty collection otherwise.</returns>
        public static ICollection<TElement?> OrEmpty<TElement>(this ICollection<TElement?>? collection)
        {
            return collection ?? [ ];
        }

        /// <summary>
        ///     Returns the collection if it is not <see langword="null" />, or an empty collection if it is
        ///     <see langword="null" />.
        /// </summary>
        /// <param name="collection">The collection to check.</param>
        /// <returns>The original collection if it is not <see langword="null" />, or an empty collection otherwise.</returns>
        public static ICollection OrEmpty(this ICollection? collection)
        {
            return collection ?? typeof(List<object>).Create<List<object>>();
        }

        /// <summary>
        ///     Executes the specified action for each element in the collection.
        /// </summary>
        /// <param name="collection">The collection to iterate over.</param>
        /// <param name="predicate">The action to execute for each element.</param>
        public static void ForEach(this ICollection? collection, Action<object?> predicate)
        {
            foreach (object? item in collection.OrEmpty())
            {
                predicate(item);
            }
        }
    }
}
