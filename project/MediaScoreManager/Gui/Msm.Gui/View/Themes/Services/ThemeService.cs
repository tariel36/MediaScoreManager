using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using Msm.Core.Extensions;
using Msm.Gui.Features.ShellFeature.Shell.Views;
using Msm.Gui.Resources.Contract;
using Msm.Gui.Resources.Models;
using Msm.Gui.View.Themes.Contract;
using Msm.Gui.View.Themes.Models;
using Msm.Gui.View.Windows.Contract;
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.MaterialDark.WPF;
using Syncfusion.Themes.MaterialDarkBlue.WPF;
using Syncfusion.Themes.MaterialLight.WPF;
using Syncfusion.Themes.MaterialLightBlue.WPF;
using Syncfusion.Themes.Office2019Black.WPF;
using Syncfusion.Themes.Office2019Colorful.WPF;
using Syncfusion.Themes.Office2019DarkGray.WPF;
using Syncfusion.Themes.Office2019HighContrast.WPF;
using Syncfusion.Themes.Office2019White.WPF;
using Syncfusion.Themes.Windows11Dark.WPF;
using Syncfusion.Themes.Windows11Light.WPF;

namespace Msm.Gui.View.Themes.Services
{
    /// <summary>
    ///     Provides functionality for managing application themes in a WPF application.
    /// </summary>
    internal class ThemeService
        : IThemeService
    {
        /// <summary>
        ///     The default application theme.
        /// </summary>
        private const AppThemes DefaultTheme = AppThemes.Windows11Light;

        /// <summary>
        ///     A collection of available themes and their user-friendly names.
        /// </summary>
        private static readonly IReadOnlyCollection<AppTheme> ThemeList = new ReadOnlyCollection<AppTheme>(
            new []
            {
                AppTheme.Create("Windows11 Light", AppThemes.Windows11Light, typeof(Windows11LightThemeSettings)),
                AppTheme.Create("Windows11 Dark", AppThemes.Windows11Dark, typeof(Windows11DarkThemeSettings)),
                AppTheme.Create("Material Light", AppThemes.MaterialLight, typeof(MaterialLightThemeSettings)),
                AppTheme.Create("Material Dark", AppThemes.MaterialDark, typeof(MaterialDarkThemeSettings)),
                AppTheme.Create("Material Light Blue", AppThemes.MaterialLightBlue, typeof(MaterialLightBlueThemeSettings)),
                AppTheme.Create("Material Dark Blue", AppThemes.MaterialDarkBlue, typeof(MaterialDarkBlueThemeSettings)),
                AppTheme.Create("Office 2019 Colorful", AppThemes.Office2019Colorful, typeof(Office2019ColorfulThemeSettings)),
                AppTheme.Create("Office 2019 Black", AppThemes.Office2019Black, typeof(Office2019BlackThemeSettings)),
                AppTheme.Create("Office 2019 White", AppThemes.Office2019White, typeof(Office2019WhiteThemeSettings)),
                AppTheme.Create("Office 2019 Dark Gray", AppThemes.Office2019DarkGray, typeof(Office2019DarkGrayThemeSettings)),
                AppTheme.Create("Office 2019 High Contrast", AppThemes.Office2019HighContrast, typeof(Office2019HighContrastThemeSettings))
            });

        /// <summary>
        ///     A collection of user-friendly theme names.
        /// </summary>
        private static readonly IReadOnlyCollection<string> ThemeNameList = new ReadOnlyCollection<string>(ThemeList.Select(static x => x.Name).ToList());

        // TODO Replace with Lock class in .NET 9
        /// <summary>
        ///     Synchronization object for thread-safe operations.
        /// </summary>
        private static readonly object SyncRoot = new();

        /// <summary>
        ///     The service for managing theme-related application properties.
        /// </summary>
        private readonly IThemeAppPropertiesService _themeAppPropertiesService;

        /// <summary>
        ///     The service for managing WPF windows.
        /// </summary>
        private readonly IWindowManagerService _windowManagerService;

        /// <summary>
        ///     The service for managing resources.
        /// </summary>
        private readonly IResourceService _resourceService;

        /// <summary>
        ///     A cache for storing themes by their names.
        /// </summary>
        private readonly Dictionary<string, Theme> _themeCache = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThemeService" /> class.
        /// </summary>
        /// <param name="themeAppPropertiesService">The service for managing theme-related application properties.</param>
        /// <param name="windowManagerService">The service for managing WPF windows.</param>
        /// <param name="resourceService">The service for managing resources.</param>
        public ThemeService(IThemeAppPropertiesService themeAppPropertiesService, IWindowManagerService windowManagerService, IResourceService resourceService)
        {
            _themeAppPropertiesService = themeAppPropertiesService;
            _windowManagerService = windowManagerService;
            _resourceService = resourceService;

            ThemeList.ForEach(SetupThemeSettings);

            SystemEvents.UserPreferenceChanging -= OnUserPreferenceChanging;
            SystemEvents.UserPreferenceChanging += OnUserPreferenceChanging;
        }

        /// <summary>
        ///     Gets a value indicating whether high contrast mode is active.
        /// </summary>
        private static bool IsHighContrastActive
        {
            get { return SystemParameters.HighContrast; }
        }

        /// <inheritdoc />
        public IReadOnlyCollection<string> GetThemeList()
        {
            return ThemeNameList;
        }

        /// <inheritdoc />
        public AppThemes UserFriendlyMap(string name)
        {
            return ThemeList.First(x => string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase)).Type;
        }

        /// <inheritdoc />
        public string UserFriendlyMap(AppThemes theme)
        {
            return ThemeList.First(x => x.Type == theme).Name;
        }

        /// <inheritdoc />
        public AppThemes Map(string name)
        {
            return Enum.Parse<AppThemes>(name);
        }

        /// <inheritdoc />
        public string Map(AppThemes theme)
        {
            return theme.ToString();
        }

        /// <inheritdoc />
        public bool SetTheme(AppThemes? theme = null)
        {
            if (IsHighContrastActive)
            {
                // TODO: Set high contrast theme
            }
            else if (theme == null)
            {
                string? themeName = _themeAppPropertiesService.Theme;

                theme = string.IsNullOrWhiteSpace(themeName)
                    ? DefaultTheme
                    : Map(themeName);
            }

            string themeToSet = Map(ObjectExtensions.SelectValue(DefaultTheme, theme));

            foreach (ShellWindow window in _windowManagerService.GetWindows<ShellWindow>())
            {
                SfSkinManager.SetTheme(window, ResolveTheme(themeToSet));
            }

            _themeAppPropertiesService.Theme = themeToSet;

            return true;
        }

        /// <inheritdoc />
        public AppThemes GetCurrentTheme()
        {
            return Map(StringExtensions.SelectValue(Map(DefaultTheme), _themeAppPropertiesService.Theme));
        }

        /// <inheritdoc />
        public string GetCurrentThemeName()
        {
            return Map(GetCurrentTheme());
        }

        /// <inheritdoc />
        public void SetupSkin(DependencyObject host)
        {
            SfSkinManager.SetTheme(host, ResolveTheme(GetCurrentThemeName()));
        }

        /// <summary>
        ///     Handles the <see cref="SystemEvents.UserPreferenceChanging" /> event to update the theme when user preferences
        ///     change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void OnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs args)
        {
            if (args.Category is not (UserPreferenceCategory.Color or UserPreferenceCategory.VisualStyle))
            {
                return;
            }

            _ = SetTheme();
        }

        /// <summary>
        ///     Get or create a theme instance by its name.
        /// </summary>
        /// <param name="name">The name of the theme.</param>
        /// <returns>The created <see cref="Theme" /> instance.</returns>
        private Theme ResolveTheme(string name)
        {
            lock (SyncRoot)
            {
                if (_themeCache.TryGetValue(name, out Theme? theme))
                {
                    return theme;
                }

                _themeCache[name] = theme = new(name);

                return theme;
            }
        }

        /// <summary>
        ///     Sets up the theme settings for the specified theme.
        /// </summary>
        /// <param name="theme">The <see cref="AppTheme" /> to set up.</param>
        private void SetupThemeSettings(AppTheme? theme)
        {
            if (theme == null)
            {
                return;
            }

            Type settingsType = theme.SettingsType;

            IThemeSetting settings = settingsType.Create().As<IThemeSetting>();

            settingsType.GetProperty(nameof(Windows11LightThemeSettings.FontFamily))
                ?.SetValue(settings, _resourceService.GetFontFamily(ResourceKeys.Fonts.DefaultFont));

            SfSkinManager.RegisterThemeSettings(Map(theme.Type), settings);
        }
    }
}
