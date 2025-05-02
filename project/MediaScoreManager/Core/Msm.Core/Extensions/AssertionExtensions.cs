// TODO Replace with new extensions in .NET 9

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Msm.Core.Assets.Internationalization;
using Msm.Core.Internationalization.Services;

namespace Msm.Core.Extensions
{
    /// <summary>
    ///     Provides extension methods for assertions and throwing exceptions with contextual information.
    /// </summary>
    public static class AssertionExtensions
    {
        /// <summary>
        ///     Represents the type category for structs.
        /// </summary>
        private const string TypeCategoryStruct = "struct";

        /// <summary>
        ///     Represents the type category for classes.
        /// </summary>
        private const string TypeCategoryClass = "class";

        /// <summary>
        ///     Asserts that the specified expression is <see langword="true" /> and throws an exception if not.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw.</typeparam>
        /// <param name="expr">The expression to check.</param>
        /// <param name="message">An optional custom message to include in the exception.</param>
        /// <param name="caller">The name of the calling member. Automatically provided by the compiler.</param>
        /// <param name="filepath">The file path of the calling code. Automatically provided by the compiler.</param>
        /// <param name="lineNbr">The line number of the calling code. Automatically provided by the compiler.</param>
        /// <param name="expression">The argument expression being checked. Automatically provided by the compiler.</param>
        /// <exception cref="TException">Thrown if the expression is <see langword="false" />.</exception>
        public static void AssertIsTrue<TException>(
            [DoesNotReturnIf(false)] this bool expr,
            string? message = null,
            [CallerMemberName] string? caller = default,
            [CallerFilePath] string? filepath = null,
            [CallerLineNumber] int? lineNbr = null,
            [CallerArgumentExpression(nameof(expr))]
            string? expression = null)
            where TException : Exception
        {
            if (expr)
            {
                return;
            }

            throw CreateException<TException>(CreateMessage(TypeCategoryStruct, message, caller, filepath, lineNbr, expression));
        }

        /// <summary>
        ///     Asserts that the specified expression is <see langword="false" /> and throws an exception if not.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw.</typeparam>
        /// <param name="expr">The expression to check.</param>
        /// <param name="message">An optional custom message to include in the exception.</param>
        /// <param name="caller">The name of the calling member. Automatically provided by the compiler.</param>
        /// <param name="filepath">The file path of the calling code. Automatically provided by the compiler.</param>
        /// <param name="lineNbr">The line number of the calling code. Automatically provided by the compiler.</param>
        /// <param name="expression">The argument expression being checked. Automatically provided by the compiler.</param>
        /// <exception cref="TException">Thrown if the expression is <see langword="true" />.</exception>
        public static void AssertIsFalse<TException>(
            [DoesNotReturnIf(false)] this bool expr,
            string? message = null,
            [CallerMemberName] string? caller = default,
            [CallerFilePath] string? filepath = null,
            [CallerLineNumber] int? lineNbr = null,
            [CallerArgumentExpression(nameof(expr))]
            string? expression = null)
            where TException : Exception
        {
            if (!expr)
            {
                return;
            }

            throw CreateException<TException>(CreateMessage(TypeCategoryStruct, message, caller, filepath, lineNbr, expression));
        }

        /// <summary>
        ///     Asserts that the specified object is not <see langword="null" />.
        /// </summary>
        /// <typeparam name="TType">The type of the object being checked.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="message">An optional custom message to include in the exception.</param>
        /// <param name="caller">The name of the calling member. Automatically provided by the compiler.</param>
        /// <param name="filepath">The file path of the calling code. Automatically provided by the compiler.</param>
        /// <param name="lineNbr">The line number of the calling code. Automatically provided by the compiler.</param>
        /// <param name="expression">The argument expression being checked. Automatically provided by the compiler.</param>
        /// <exception cref="ArgumentNullException">Thrown if the object is <see langword="null" />.</exception>
        public static void AssertNotNull<TType>(
            [NotNull] this TType? obj,
            string? message = null,
            [CallerMemberName] string? caller = default,
            [CallerFilePath] string? filepath = null,
            [CallerLineNumber] int? lineNbr = null,
            [CallerArgumentExpression(nameof(obj))]
            string? expression = null)
            where TType : class
        {
            if (obj == null)
            {
                throw CreateException<ArgumentNullException>(CreateMessage(TypeCategoryClass, message, caller, filepath, lineNbr, expression));
            }
        }

        /// <summary>
        ///     Throws a specified exception if the <paramref name="obj"/> is <see langword="null" /> or whitespace, including caller context information in the
        ///     exception message.
        /// </summary>
        /// <param name="obj">The object to check for <see langword="null" /> or whitespace.</param>
        /// <param name="message">An optional custom message to include in the exception.</param>
        /// <param name="caller">The name of the calling member. Automatically provided by the compiler.</param>
        /// <param name="filepath">The file path of the calling code. Automatically provided by the compiler.</param>
        /// <param name="lineNbr">The line number of the calling code. Automatically provided by the compiler.</param>
        /// <param name="expression">The argument expression being checked. Automatically provided by the compiler.</param>
        /// <returns>The original object if it is not <see langword="null" /> or whitespace.</returns>
        /// <exception cref="NullReferenceException">Thrown if the <paramref name="obj"/> is <see langword="null" /> or whitespace.</exception>
        public static string OrCallerThrow(
            [NotNull] this string? obj,
            string? message = null,
            [CallerMemberName] string? caller = default,
            [CallerFilePath] string? filepath = null,
            [CallerLineNumber] int? lineNbr = null,
            [CallerArgumentExpression(nameof(obj))]
            string? expression = null)
        {
            return string.IsNullOrWhiteSpace(obj)
                ? throw CreateException<NullReferenceException>(CreateMessage(TypeCategoryClass, message, caller, filepath, lineNbr, expression))
                : obj;
        }

        /// <summary>
        ///     Throws a specified exception if the object is <see langword="null" />, including caller context information in the
        ///     exception message.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw.</typeparam>
        /// <typeparam name="TType">The type of the object being checked.</typeparam>
        /// <param name="obj">The object to check for <see langword="null" />.</param>
        /// <param name="message">An optional custom message to include in the exception.</param>
        /// <param name="caller">The name of the calling member. Automatically provided by the compiler.</param>
        /// <param name="filepath">The file path of the calling code. Automatically provided by the compiler.</param>
        /// <param name="lineNbr">The line number of the calling code. Automatically provided by the compiler.</param>
        /// <param name="expression">The argument expression being checked. Automatically provided by the compiler.</param>
        /// <returns>The original object if it is not <see langword="null" />.</returns>
        /// <exception cref="TException">Thrown if the object is <see langword="null" />.</exception>
        public static TType OrCallerThrow<TException, TType>(
            [NotNull] this TType? obj,
            string? message = null,
            [CallerMemberName] string? caller = default,
            [CallerFilePath] string? filepath = null,
            [CallerLineNumber] int? lineNbr = null,
            [CallerArgumentExpression(nameof(obj))]
            string? expression = null)
            where TType : class
            where TException : Exception
        {
            return obj ?? throw CreateException<TException>(CreateMessage(TypeCategoryClass, message, caller, filepath, lineNbr, expression));
        }

        /// <summary>
        ///     Throws an exception if the object is <see langword="null" />, including caller context information in the
        ///     exception message.
        /// </summary>
        /// <typeparam name="TType">The type of the object being checked.</typeparam>
        /// <param name="obj">The object to check for <see langword="null" />.</param>
        /// <param name="message">An optional custom message to include in the exception.</param>
        /// <param name="caller">The name of the calling member. Automatically provided by the compiler.</param>
        /// <param name="filepath">The file path of the calling code. Automatically provided by the compiler.</param>
        /// <param name="lineNbr">The line number of the calling code. Automatically provided by the compiler.</param>
        /// <param name="expression">The argument expression being checked. Automatically provided by the compiler.</param>
        /// <returns>The original object if it is not <see langword="null" />.</returns>
        public static TType OrCallerThrow<TType>(
            [NotNull] this TType? obj,
            string? message = null,
            [CallerMemberName] string? caller = default,
            [CallerFilePath] string? filepath = null,
            [CallerLineNumber] int? lineNbr = null,
            [CallerArgumentExpression(nameof(obj))]
            string? expression = null)
            where TType : class
        {
            return obj ?? throw CreateException<NullReferenceException>(CreateMessage(TypeCategoryClass, message, caller, filepath, lineNbr, expression));
        }

        /// <summary>
        ///     Throws an exception if the object is <see langword="null" />, including caller context information in the
        ///     exception message.
        /// </summary>
        /// <typeparam name="TType">The type of the object being checked.</typeparam>
        /// <param name="obj">The object to check for <see langword="null" />.</param>
        /// <param name="message">Optional provider for custom message to include in the exception.</param>
        /// <param name="caller">The name of the calling member. Automatically provided by the compiler.</param>
        /// <param name="filepath">The file path of the calling code. Automatically provided by the compiler.</param>
        /// <param name="lineNbr">The line number of the calling code. Automatically provided by the compiler.</param>
        /// <param name="expression">The argument expression being checked. Automatically provided by the compiler.</param>
        /// <returns>The original object if it is not <see langword="null" />.</returns>
        public static TType OrCallerThrow<TType>(
            [NotNull] this TType? obj,
            Func<string?>? message = null,
            [CallerMemberName] string? caller = default,
            [CallerFilePath] string? filepath = null,
            [CallerLineNumber] int? lineNbr = null,
            [CallerArgumentExpression(nameof(obj))]
            string? expression = null)
            where TType : class
        {
            return obj ?? throw CreateException<NullReferenceException>(CreateMessage(TypeCategoryClass, message?.Invoke(), caller, filepath, lineNbr, expression));
        }

        /// <summary>
        ///     Throws a specified exception if the nullable value type is <see langword="null" />, including caller context
        ///     information in the exception message.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw.</typeparam>
        /// <typeparam name="TType">The type of the value being checked.</typeparam>
        /// <param name="obj">The nullable value to check for <see langword="null" />.</param>
        /// <param name="message">An optional custom message to include in the exception.</param>
        /// <param name="caller">The name of the calling member. Automatically provided by the compiler.</param>
        /// <param name="filepath">The file path of the calling code. Automatically provided by the compiler.</param>
        /// <param name="lineNbr">The line number of the calling code. Automatically provided by the compiler.</param>
        /// <param name="expression">The argument expression being checked. Automatically provided by the compiler.</param>
        /// <returns>The original value if it is not <see langword="null" />.</returns>
        /// <exception cref="TException">Thrown if the value is <see langword="null" />.</exception>
        public static TType OrCallerThrow<TException, TType>(
            [NotNull] this TType? obj,
            string? message = null,
            [CallerMemberName] string? caller = default,
            [CallerFilePath] string? filepath = null,
            [CallerLineNumber] int? lineNbr = null,
            [CallerArgumentExpression(nameof(obj))]
            string? expression = null)
            where TType : struct
            where TException : Exception
        {
            return obj ?? throw CreateException<TException>(CreateMessage(TypeCategoryStruct, message, caller, filepath, lineNbr, expression));
        }

        /// <summary>
        ///     Creates a detailed exception message including optional custom text and caller context information.
        /// </summary>
        /// <param name="category">The type category (e.g., "class" or "struct").</param>
        /// <param name="message">An optional custom message to include.</param>
        /// <param name="caller">The name of the calling member.</param>
        /// <param name="filePath">The file path of the calling code.</param>
        /// <param name="lineNbr">The line number of the calling code.</param>
        /// <param name="expression">The argument expression being checked.</param>
        /// <returns>A formatted exception message string.</returns>
        private static string CreateMessage(string category, string? message, string? caller, string? filePath, int? lineNbr, string? expression)
        {
            return new StringBuilder(InternalTranslationService.Instance.GetValueOrDefault(static () => Translations.Assertion_TypeCategory, category))
                .AppendLineIf(!string.IsNullOrWhiteSpace(message), message)
                .AppendLineIf(!string.IsNullOrWhiteSpace(caller), InternalTranslationService.Instance.GetValueOrDefault(static () => Translations.Assertion_Caller, caller))
                .AppendLineIf(!string.IsNullOrWhiteSpace(filePath), InternalTranslationService.Instance.GetValueOrDefault(static () => Translations.Assertion_File, filePath))
                .AppendLineIf(lineNbr.HasValue, () => InternalTranslationService.Instance.GetValueOrDefault(static () => Translations.Assertion_Line, lineNbr))
                .AppendLineIf(!string.IsNullOrWhiteSpace(expression), InternalTranslationService.Instance.GetValueOrDefault(static () => Translations.Assertion_Expression, expression))
                .ToString();
        }

        /// <summary>
        ///     Creates an instance of the specified exception type with an optional message.
        /// </summary>
        /// <typeparam name="TException">The type of exception to create.</typeparam>
        /// <param name="message">An optional message to include in the exception.</param>
        /// <returns>An instance of the specified exception type.</returns>
        private static Exception CreateException<TException>(string? message = null)
            where TException : Exception
        {
            return typeof(TException).GetConstructor(BindingFlags.Public, [ typeof(string) ])
                       ?.Invoke([ message ]) as TException
                   ?? (Exception) new NullReferenceException(message);
        }
    }
}
