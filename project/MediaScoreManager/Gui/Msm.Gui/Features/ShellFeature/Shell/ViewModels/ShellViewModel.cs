using System.Windows.Input;
using Msm.Core.Wpf.Commands;
using Msm.Core.Wpf.ViewModels.Base;
using Msm.Gui.Features.RightPaneFeature.RightPane.Contract;
using Msm.Gui.View.Navigation.Contract;

namespace Msm.Gui.Features.ShellFeature.Shell.ViewModels
{
    /// <summary>
    ///     Represents the view model for the shell window in the WPF application.
    /// </summary>
    internal class ShellViewModel
        : WindowViewModel
    {
        /// <summary>
        ///     The navigation service for managing navigation within the application.
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        ///     The service for managing the right pane in the application.
        /// </summary>
        private readonly IRightPaneService _rightPaneService;

        /// <summary>
        ///     The command executed when the shell window is loaded.
        /// </summary>
        private ICommand _cmdLoaded;

        /// <summary>
        ///     The command executed when the shell window is unloaded.
        /// </summary>
        private ICommand _cmdUnloaded;

        /// <summary>
        ///     The command executed when the view selection is changed.
        /// </summary>
        private ICommand _cmdViewSelectionChanged;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ShellViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service for managing navigation.</param>
        /// <param name="rightPaneService">The service for managing the right pane.</param>
        public ShellViewModel(INavigationService navigationService, IRightPaneService rightPaneService)
        {
            _navigationService = navigationService;
            _rightPaneService = rightPaneService;

            _cmdLoaded = new RelayCommand(Loaded);
            _cmdUnloaded = new RelayCommand(Unloaded);
            _cmdViewSelectionChanged = new RelayCommand(ViewSelectionChanged);
        }

        /// <summary>
        ///     Gets or sets the command executed when the view selection is changed.
        /// </summary>
        public ICommand CmdViewSelectionChanged
        {
            get { return _cmdViewSelectionChanged; }
            set { _ = Set(ref _cmdViewSelectionChanged, value); }
        }

        /// <summary>
        ///     Gets or sets the command executed when the shell window is unloaded.
        /// </summary>
        public ICommand CmdUnloaded
        {
            get { return _cmdUnloaded; }
            set { _ = Set(ref _cmdUnloaded, value); }
        }

        /// <summary>
        ///     Gets or sets the command executed when the shell window is loaded.
        /// </summary>
        public ICommand CmdLoaded
        {
            get { return _cmdLoaded; }
            set { _ = Set(ref _cmdLoaded, value); }
        }

        /// <summary>
        ///     Handles the logic when the shell window is loaded.
        /// </summary>
        private void Loaded()
        {
            // Ignore
        }

        /// <summary>
        ///     Handles the logic when the shell window is unloaded.
        /// </summary>
        private void Unloaded()
        {
            _rightPaneService.CleanUp();
        }

        /// <summary>
        ///     Handles the logic when the view selection is changed.
        /// </summary>
        /// <param name="arg">The argument representing the selected view.</param>
        private void ViewSelectionChanged(object? arg)
        {
            _ = _navigationService.NavigateTo(arg?.ToString(), null, true);
        }
    }
}
