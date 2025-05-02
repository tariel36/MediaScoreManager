using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Msm.Core.Assets.Internationalization;
using Msm.Core.Extensions;
using Msm.Core.Internationalization.Services;
using Msm.Core.Stability.Providers;

namespace Msm.Core.Infrastructure.Configuration.Services
{
    /// <summary>
    /// Provides base functionality for retrieving application settings from the configuration.
    /// </summary>
    /// <param name="configuration">The configuration instance used to retrieve settings.</param>
    public abstract class BaseAppSettingsService(IConfiguration configuration)
    {
        /// <summary>
        /// Retrieves a required collection of values from the configuration section specified by the caller's member name.
        /// </summary>
        /// <typeparam name="TModel">The type of the elements in the collection.</typeparam>
        /// <param name="key">The key of the configuration section. Automatically provided by the caller's member name.</param>
        /// <returns>A read-only collection of values.</returns>
        /// <exception cref="NullReferenceException">Thrown if the configuration section is not found.</exception>
        protected IReadOnlyCollection<TModel?> GetRequiredCollection<TModel>([CallerMemberName] string? key = null)
        {
            return (configuration.GetSection(key.OrCallerThrow()).Get<List<TModel>>() as IReadOnlyCollection<TModel?>).OrEmpty();
        }

        /// <summary>
        /// Retrieves a required string value from the configuration specified by the caller's member name.
        /// </summary>
        /// <param name="key">The key of the configuration value. Automatically provided by the caller's member name.</param>
        /// <returns>The required string value.</returns>
        /// <exception cref="NullReferenceException">Thrown if the configuration value is not found.</exception>
        protected string GetRequiredValue([CallerMemberName] string? key = null)
        {
            return configuration[key.OrCallerThrow()].OrCallerThrow();
        }

        /// <summary>
        /// Retrieves a required value of the specified type from the configuration specified by the caller's member name.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to retrieve.</typeparam>
        /// <param name="key">The key of the configuration value. Automatically provided by the caller's member name.</param>
        /// <returns>The required value of the specified type.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the value cannot be converted to the specified type.</exception>
        protected TValue GetRequiredValue<TValue>([CallerMemberName] string? key = null)
            where TValue : struct
        {
            return ChangeType<TValue>(key, GetRequiredValue(key));
        }

        /// <summary>
        /// Retrieves an optional string value from the configuration specified by the caller's member name.
        /// </summary>
        /// <param name="key">The key of the configuration value. Automatically provided by the caller's member name.</param>
        /// <returns>The string value, or an empty string if the value is not found.</returns>
        protected string GetValueOrDefault([CallerMemberName] string? key = null)
        {
            return configuration[key.OrCallerThrow()].OrEmpty();
        }

        /// <summary>
        /// Retrieves an optional value of the specified type from the configuration specified by the caller's member name.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to retrieve.</typeparam>
        /// <param name="key">The key of the configuration value. Automatically provided by the caller's member name.</param>
        /// <returns>The value of the specified type, or the default value of the type if the configuration value is not found.</returns>
        protected TValue GetValueOrDefault<TValue>([CallerMemberName] string? key = null)
            where TValue : struct
        {
            string? sValue = configuration[key.OrCallerThrow()];

            if (string.IsNullOrWhiteSpace(sValue))
            {
                return NullObjectProvider.Instance.Resolve<TValue>();
            }

            return ChangeType<TValue>(key, sValue);
        }

        /// <summary>
        /// Converts a string value to the specified type.
        /// </summary>
        /// <typeparam name="TValue">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration value.</param>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The converted value of the specified type.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the value cannot be converted to the specified type.</exception>
        private static TValue ChangeType<TValue>(string? key, string value)
            where TValue : struct
        {
            Type valueType = typeof(TValue);

            Type? genericArg = valueType.GetGenericArguments().FirstOrDefault();

            try
            {
                return genericArg == default
                    ? (TValue) Convert.ChangeType(value, valueType)
                    : (TValue) Convert.ChangeType(value, genericArg);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(InternalTranslationService.Instance.GetValueOrDefault(static () => Translations.FailedToParseAppSettingsKey, key), ex);
            }
        }
    }
}
