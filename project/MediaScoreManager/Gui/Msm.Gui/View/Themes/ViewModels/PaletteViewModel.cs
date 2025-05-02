using System.Windows.Media;
using Msm.Core.Wpf.ViewModels.Base;

namespace Msm.Gui.View.Themes.ViewModels
{
    /// <summary>
    /// A view model for a palette.
    /// </summary>
    internal class PaletteViewModel
        : DataViewModel
    {
        /// <summary>
        /// Denotes the palette name.
        /// </summary>
        private string? _name;

        /// <summary>
        /// Denotes the theme name.
        /// </summary>
        private string? _theme;

        /// <summary>
        /// Denotes the palette primary background brush.
        /// </summary>
        private Brush? _primaryBackground;

        /// <summary>
        /// Denotes the palette primary foreground brush.
        /// </summary>
        private Brush? _primaryForeground;

        /// <summary>
        /// Denotes the palette primary alternate background brush.
        /// </summary>
        private Brush? _primaryBackgroundAlt;

        /// <summary>
        /// Denotes the name to be displayed in the UI.
        /// </summary>
        private string? _displayName;

        /// <summary>
        /// Denotes the primary border color brush.
        /// </summary>
        private Brush? _primaryBorderColor;

        /// <summary>
        /// Denotes the palette name.
        /// </summary>
        public string? Name
        {
            get { return _name; }
            set { _ = Set(ref _name, value); }
        }

        /// <summary>
        /// Denotes the theme name.
        /// </summary>
        public string? Theme
        {
            get { return _theme; }
            set { _ = Set(ref _theme, value); }
        }

        /// <summary>
        /// Denotes the palette primary background brush.
        /// </summary>
        public Brush? PrimaryBackground
        {
            get { return _primaryBackground; }
            set { _ = Set(ref _primaryBackground, value); }
        }

        /// <summary>
        /// Denotes the palette primary foreground brush.
        /// </summary>
        public Brush? PrimaryForeground
        {
            get { return _primaryForeground; }
            set { _ = Set(ref _primaryForeground, value); }
        }

        /// <summary>
        /// Denotes the palette primary alternate background brush.
        /// </summary>
        public Brush? PrimaryBackgroundAlt
        {
            get { return _primaryBackgroundAlt; }
            set { _ = Set(ref _primaryBackgroundAlt, value); }
        }

        /// <summary>
        /// Denotes the name to be displayed in the UI.
        /// </summary>
        public string? DisplayName
        {
            get { return _displayName; }
            set { _ = Set(ref _displayName, value); }
        }

        /// <summary>
        /// Denotes the primary border color brush.
        /// </summary>
        public Brush? PrimaryBorderColor
        {
            get { return _primaryBorderColor; }
            set { _ = Set(ref _primaryBorderColor, value); }
        }
    }
}
