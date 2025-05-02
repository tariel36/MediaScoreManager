namespace Msm.Gui.View.Themes.Contract
{
    /// <summary>
    /// Defines a contract for managing theme-related application properties.
    /// </summary>
    internal interface IThemeAppPropertiesService
    {
        /// <summary>
        /// Gets or sets the current theme of the application.
        /// </summary>
        string? Theme { get; set; }
    }
}
