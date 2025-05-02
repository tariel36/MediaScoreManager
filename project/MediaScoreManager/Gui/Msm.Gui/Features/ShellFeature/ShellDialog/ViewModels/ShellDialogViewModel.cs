using System;
using System.Windows.Input;
using Msm.Core.Wpf.Commands;
using Msm.Core.Wpf.ViewModels.Base;

namespace Msm.Gui.Features.ShellFeature.ShellDialog.ViewModels
{
    /// <summary>
    /// Represents the view model for the shell dialog window in the WPF application.
    /// </summary>
    internal class ShellDialogViewModel
        : WindowViewModel
    {
        /// <summary>
        /// The command executed to close the dialog.
        /// </summary>
        private ICommand _cmdClose;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellDialogViewModel"/> class.
        /// </summary>
        public ShellDialogViewModel()
        {
            _cmdClose = new RelayCommand(OnClose);
        }

        /// <summary>
        /// Gets or sets the command executed to close the dialog.
        /// </summary>
        public ICommand CmdClose
        {
            get { return _cmdClose; }
            set { _ = Set(ref _cmdClose, value); }
        }

        /// <summary>
        /// Gets or sets the action to set the result of the dialog.
        /// </summary>
        internal Action<bool?>? SetResult { get; set; }

        /// <summary>
        /// Handles the logic for closing the dialog.
        /// </summary>
        private void OnClose()
        {
            if (SetResult == null)
            {
                return;
            }

            SetResult(true);
        }
    }
}
