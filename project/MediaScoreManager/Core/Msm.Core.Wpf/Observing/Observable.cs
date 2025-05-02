using System.ComponentModel;
using System.Runtime.CompilerServices;
using Msm.Core.Extensions;

namespace Msm.Core.Wpf.Observing
{
    /// <summary>
    ///     Provides a base class for objects that support property change notifications, implementing
    ///     <see cref="INotifyPropertyChanged" /> and <see cref="INotifyPropertyChanging" />.
    /// </summary>
    public class Observable
        : INotifyPropertyChanged,
          INotifyPropertyChanging
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Observable" /> class.
        /// </summary>
        protected Observable()
        {
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        ///     Occurs when a property value is about to change.
        /// </summary>
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <summary>
        ///     Sets the value of a field and raises the <see cref="PropertyChanged" /> event if the value has changed.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">The field to set.</param>
        /// <param name="value">The new value to assign to the field.</param>
        /// <param name="propertyName">The name of the property. Automatically provided by the compiler.</param>
        /// <returns><see langword="true" /> if the value was changed; otherwise, <see langword="false" />.</returns>
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            return SetInternal(ref field, value, null, propertyName);
        }

        /// <summary>
        ///     Sets the value of a field, raises the <see cref="PropertyChanged" /> event, and optionally raises events for
        ///     dependent properties.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">The field to set.</param>
        /// <param name="value">The new value to assign to the field.</param>
        /// <param name="dependantProperties">An array of dependent property names to raise events for.</param>
        /// <param name="propertyName">The name of the property. Automatically provided by the compiler.</param>
        /// <returns><see langword="true" /> if the value was changed; otherwise, <see langword="false" />.</returns>
        protected bool Set<T>(ref T field, T value, string []? dependantProperties, [CallerMemberName] string? propertyName = null)
        {
            return SetInternal(ref field, value, dependantProperties, propertyName);
        }

        /// <summary>
        ///     Sets the value of a property using getter and setter functions, and raises the <see cref="PropertyChanged" /> event
        ///     if the value has changed.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="getter">The function to get the current value of the property.</param>
        /// <param name="setter">The function to set the new value of the property.</param>
        /// <param name="value">The new value to assign to the property.</param>
        /// <param name="propertyName">The name of the property. Automatically provided by the compiler.</param>
        /// <returns><see langword="true" /> if the value was changed; otherwise, <see langword="false" />.</returns>
        protected bool Set<T>(Func<T>? getter, Action<T>? setter, T value, [CallerMemberName] string? propertyName = null)
        {
            if (getter == null || setter == null || string.IsNullOrWhiteSpace(propertyName) || Equals(getter(), value))
            {
                return false;
            }

            setter(value);

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        ///     Raises the <see cref="PropertyChanged" /> event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property. Automatically provided by the compiler.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        /// <summary>
        ///     Raises the <see cref="PropertyChanging" /> event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property. Automatically provided by the compiler.</param>
        protected virtual void OnPropertyChanging([CallerMemberName] string? propertyName = null)
        {
            PropertyChanging?.Invoke(this, new(propertyName));
        }

        /// <summary>
        ///     Sets the value of a field, raises the <see cref="PropertyChanged" /> event, and optionally raises events for
        ///     dependent properties.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">The field to set.</param>
        /// <param name="value">The new value to assign to the field.</param>
        /// <param name="dependantProperties">An array of dependent property names to raise events for.</param>
        /// <param name="propertyName">The name of the property. Automatically provided by the compiler.</param>
        /// <returns><see langword="true" /> if the value was changed; otherwise, <see langword="false" />.</returns>
        private bool SetInternal<T>(ref T field, T value, string []? dependantProperties, string? propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName) || Equals(field, value))
            {
                return false;
            }

            field = value;

            OnPropertyChanged(propertyName);

            if (dependantProperties.IsNullOrEmpty())
            {
                return true;
            }

            foreach (string dependantPropertyName in dependantProperties)
            {
                OnPropertyChanged(dependantPropertyName);
            }

            return true;
        }
    }
}
