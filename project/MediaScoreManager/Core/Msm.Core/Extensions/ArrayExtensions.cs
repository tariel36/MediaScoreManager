// TODO Replace with new extensions in .NET 9

namespace Msm.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with arrays.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Creates a new array containing the specified elements.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the array.</typeparam>
        /// <param name="elements">The elements to include in the array.</param>
        /// <returns>A new array containing the specified elements.</returns>
        public static TElement [] Create<TElement>(params TElement [] elements)
        {
            // TODO Use IEnumerable<TElement> instead of params in .NET 9
            return elements;
        }
    }
}
