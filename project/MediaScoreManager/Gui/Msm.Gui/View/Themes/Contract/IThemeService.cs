using System.Collections.Generic;
using System.Windows;
using Msm.Gui.View.Themes.Models;

namespace Msm.Gui.View.Themes.Contract
{
    /// <summary>
    /// Defines a contract for managing application themes in a WPF application.
    /// </summary>
    internal interface IThemeService
    {
        /// <summary>
        /// Retrieves a list of available themes.
        /// </summary>
        /// <returns>A read-only collection of theme names.</returns>
        IReadOnlyCollection<string> GetThemeList();

        /// <summary>
        /// Maps a user-friendly theme name to its corresponding <see cref="AppThemes"/> value.
        /// </summary>
        /// <param name="name">The user-friendly name of the theme.</param>
        /// <returns>The corresponding <see cref="AppThemes"/> value.</returns>
        AppThemes UserFriendlyMap(string name);

        /// <summary>
        /// Maps an <see cref="AppThemes"/> value to its user-friendly theme name.
        /// </summary>
        /// <param name="theme">The <see cref="AppThemes"/> value to map.</param>
        /// <returns>The user-friendly name of the theme.</returns>
        string UserFriendlyMap(AppThemes theme);

        /// <summary>
        /// Maps a theme name to its corresponding <see cref="AppThemes"/> value.
        /// </summary>
        /// <param name="name">The name of the theme.</param>
        /// <returns>The corresponding <see cref="AppThemes"/> value.</returns>
        AppThemes Map(string name);

        /// <summary>
        /// Maps an <see cref="AppThemes"/> value to its theme name.
        /// </summary>
        /// <param name="theme">The <see cref="AppThemes"/> value to map.</param>
        /// <returns>The name of the theme.</returns>
        string Map(AppThemes theme);

        /// <summary>
        /// Sets the current application theme.
        /// </summary>
        /// <param name="theme">The <see cref="AppThemes"/> value to set. If <see langword="null"/>, the default theme is used.</param>
        /// <returns><see langword="true"/> if the theme was successfully set; otherwise, <see langword="false"/>.</returns>
        bool SetTheme(AppThemes? theme = null);

        /// <summary>
        /// Retrieves the current application theme.
        /// </summary>
        /// <returns>The current <see cref="AppThemes"/> value.</returns>
        AppThemes GetCurrentTheme();

        /// <summary>
        /// Retrieves the current application theme.
        /// </summary>
        /// <returns>The current theme as <see lanngword="string" />.</returns>
        string GetCurrentThemeName();

        /// <summary>
        /// Setups the skin in the external manager.
        /// </summary>
        /// <param name="host">The host for the skin.</param>
        void SetupSkin(DependencyObject host);
    }
}