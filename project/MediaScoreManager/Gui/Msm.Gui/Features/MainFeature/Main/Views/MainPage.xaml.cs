using Msm.Gui.Features.MainFeature.Main.ViewModels;

namespace Msm.Gui.Features.MainFeature.Main.Views
{
    /// <summary>
    /// Represents the main page in the WPF application.
    /// </summary>
    internal partial class MainPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model for the main page.</param>
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
