using System.Linq.Expressions;

namespace Msm.Core.Internationalization.Contract
{
    /// <summary>
    /// Defines a contract for translation services to retrieve localized strings.
    /// </summary>
    public interface ITranslationService
    {
        /// <summary>
        /// Retrieves a translation value based on the provided key expression.
        /// </summary>
        /// <param name="keyProvider">An expression that provides the translation key.</param>
        /// <returns>The translated value, or a default value if the key is not found.</returns>
        string GetValueOrDefault(Expression<Func<string>> keyProvider);

        /// <summary>
        /// Retrieves a translation value based on the provided key expression and replaces placeholders with the specified arguments.
        /// </summary>
        /// <param name="keyProvider">An expression that provides the translation key.</param>
        /// <param name="args">A dictionary of arguments to replace placeholders in the translation.</param>
        /// <returns>The translated value with placeholders replaced, or a default value if the key is not found.</returns>
        string GetValueOrDefault(Expression<Func<string>> keyProvider, Dictionary<string, string> args);

        // TODO Use IEnumerable<TElement> instead of params in .NET 9
        /// <summary>
        /// Retrieves a translation value based on the provided key expression and formats it with the specified arguments.
        /// </summary>
        /// <param name="keyProvider">An expression that provides the translation key.</param>
        /// <param name="args">An array of arguments to format the translation.</param>
        /// <returns>The formatted translated value, or a default value if the key is not found.</returns>
        string GetValueOrDefault(Expression<Func<string>> keyProvider, params object? [] args);
    }
}
