using System;

namespace Msm.Gui.View.Themes.Models
{
    /// <summary>
    /// Represents an application theme with its associated properties.
    /// </summary>
    internal class AppTheme
    {
        /// <summary>
        /// Gets the name of the theme.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets the type of the theme.
        /// </summary>
        public required AppThemes Type { get; init; }

        /// <summary>
        /// Gets the type of the settings associated with the theme.
        /// </summary>
        public required Type SettingsType { get; init; }

        /// <summary>
        /// Creates a new instance of the <see cref="AppTheme"/> class with the specified properties.
        /// </summary>
        /// <param name="name">The name of the theme.</param>
        /// <param name="type">The type of the theme.</param>
        /// <param name="settingsType">The type of the settings associated with the theme.</param>
        /// <returns>A new instance of the <see cref="AppTheme"/> class.</returns>
        public static AppTheme Create(string name, AppThemes type, Type settingsType)
        {
            return new()
            {
                Name = name,
                Type = type,
                SettingsType = settingsType
            };
        }
    }
}
