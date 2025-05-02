using System.Windows.Controls;
using Msm.Gui.Features.ShellFeature.Shell.Contract;
using Msm.Gui.Features.ShellFeature.Shell.ViewModels;
using Msm.Gui.View.Themes.Contract;

namespace Msm.Gui.Features.ShellFeature.Shell.Views
{
    /// <summary>
    /// Represents the shell window in the WPF application.
    /// </summary>
    internal partial class ShellWindow
        : IShellWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellWindow"/> class.
        /// </summary>
        /// <param name="themeService">The service for managing application themes.</param>
        /// <param name="viewModel">The view model for the shell window.</param>
        public ShellWindow(IThemeService themeService, ShellViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

            themeService.SetupSkin(this);
        }

        /// <inheritdoc />
        public Frame GetNavigationFrame()
        {
            return FShellFrame;
        }

        /// <inheritdoc />
        public void ShowWindow()
        {
            Show();
        }

        /// <inheritdoc />
        public void CloseWindow()
        {
            Close();
        }
    }
}
