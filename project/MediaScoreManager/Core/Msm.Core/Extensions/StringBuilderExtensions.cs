// TODO Replace with new extensions in .NET 9

using System.Text;

namespace Msm.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="StringBuilder"/> class.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends a new line with the specified string to the <see cref="StringBuilder"/> if the given condition is <see langword="true" />.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to append to.</param>
        /// <param name="check">A condition that determines whether the string should be appended.</param>
        /// <param name="toAppend">The string to append if the condition is <see langword="true" />.</param>
        /// <returns>The original <see cref="StringBuilder"/> instance with the appended string if the condition is met.</returns>
        public static StringBuilder AppendLineIf(this StringBuilder sb, bool check, string? toAppend)
        {
            return check
                ? sb.AppendLine(toAppend)
                : sb;
        }

        /// <summary>
        /// Appends a new line with the result of the specified function to the <see cref="StringBuilder"/> if the given condition is <see langword="true" />.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to append to.</param>
        /// <param name="check">A condition that determines whether the string should be appended.</param>
        /// <param name="toAppend">A function that produces the string to append if the condition is <see langword="true" />.</param>
        /// <returns>The original <see cref="StringBuilder"/> instance with the appended string if the condition is met.</returns>
        public static StringBuilder AppendLineIf(this StringBuilder sb, bool check, Func<string?> toAppend)
        {
            return check
                ? sb.AppendLine(toAppend())
                : sb;
        }
    }
}
