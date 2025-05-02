// TODO Replace with new extensions in .NET 9

using System.Linq.Expressions;
using System.Reflection;
using Msm.Core.Assets.Internationalization;
using Msm.Core.Internationalization.Services;
using Msm.Core.Stability.Providers;

namespace Msm.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for reflection-related operations.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// The length of an empty collection.
        /// </summary>
        private const int ZeroLength = 0;

        /// <summary>
        /// Creates an instance of the specified type if it matches the expected type.
        /// </summary>
        /// <typeparam name="TType">The expected type of the instance to create.</typeparam>
        /// <param name="type">The type to create an instance of.</param>
        /// <returns>An instance of the specified type, or <see langword="null" /> if the type is <see langword="null" />.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the specified type does not match the expected type.</exception>
        public static TType Create<TType>(this Type? type)
        {
            type.AssertNotNull();

            Type actualType = typeof(TType);

            if (type != actualType)
            {
                throw new InvalidOperationException(
                    InternalTranslationService.Instance.GetValueOrDefault(
                        static () => Translations.InputAndOutputTypeAreDifferent,
                        type.Name,
                        actualType.Name));
            }

            return (TType) Create(actualType);
        }

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <param name="type">The type to create an instance of.</param>
        /// <returns>An instance of the specified type, or <see langword="null" /> if the type is <see langword="null" />.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the type does not have a parameterless constructor.</exception>
        public static object Create(this Type? type)
        {
            type.AssertNotNull();

            if (type.IsEnum)
            {
                return Activator.CreateInstance(type).OrCallerThrow<InvalidOperationException, object>();
            }

            if (type.IsValueType)
            {
                return Activator.CreateInstance(type).OrCallerThrow<InvalidOperationException, object>();
            }

            if (type == typeof(string))
            {
                return string.Empty;
            }

            if (type.IsArray)
            {
                return Array.CreateInstance(type.GetElementType().OrCallerThrow(() => type.Name), ZeroLength);
            }

            type.IsInterface.AssertIsFalse<InvalidOperationException>();
            type.IsAbstract.AssertIsFalse<InvalidOperationException>();

            ConstructorInfo? ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(static x => x.GetParameters().IsNullOrEmpty());

            return ctor.OrCallerThrow(
                    () => InternalTranslationService.Instance.GetValueOrDefault(
                        static () => Translations.ParameterlessConstructorNotFoundForType,
                        type.Name))
                .Invoke(NullObjectProvider.Instance.Resolve<object []>());
        }

        /// <summary>
        /// Extracts the name of a property from a property expression.
        /// </summary>
        /// <typeparam name="TType">The type of the object containing the property.</typeparam>
        /// <param name="propertyExpression">An expression representing the property.</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the expression body is not a <see cref="MemberExpression"/>.</exception>
        public static string ExtractPropertyName<TType>(Expression<Func<TType, string?>> propertyExpression)
        {
            if (propertyExpression.Body is not MemberExpression memberExpression)
            {
                throw new InvalidOperationException(
                    InternalTranslationService.Instance.GetValueOrDefault(
                        static () => Translations.ExpressionBodyIsNotMemberExpression,
                        nameof(propertyExpression),
                        nameof(MemberExpression),
                        propertyExpression.Body.Type.Name
                    ));
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Gets the name of a property from a property expression.
        /// </summary>
        /// <typeparam name="TType">The type of the object containing the property.</typeparam>
        /// <param name="propertyExpression">An expression representing the property.</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the expression body is not a <see cref="MemberExpression"/>.</exception>
        public static string GetPropertyName<TType>(this Expression<Func<TType, string?>> propertyExpression)
        {
            if (propertyExpression.Body is not MemberExpression memberExpression)
            {
                throw new InvalidOperationException(
                    InternalTranslationService.Instance.GetValueOrDefault(
                        static () => Translations.ExpressionBodyIsNotMemberExpression,
                        nameof(propertyExpression),
                        nameof(MemberExpression),
                        propertyExpression.Body.Type.Name
                    ));
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Gets the name of a property from a property expression.
        /// </summary>
        /// <param name="propertyExpression">An expression representing the property.</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the expression body is not a <see cref="MemberExpression"/>.</exception>
        public static string GetPropertyName(this Expression<Func<string>> propertyExpression)
        {
            if (propertyExpression.Body is not MemberExpression memberExpression)
            {
                throw new InvalidOperationException(
                    InternalTranslationService.Instance.GetValueOrDefault(
                        static () => Translations.ExpressionBodyIsNotMemberExpression,
                        nameof(propertyExpression),
                        nameof(MemberExpression),
                        propertyExpression.Body.Type.Name
                    ));
            }

            return memberExpression.Member.Name;
        }
    }
}
