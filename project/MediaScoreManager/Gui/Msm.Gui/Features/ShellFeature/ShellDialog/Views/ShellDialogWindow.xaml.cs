using System.Windows.Controls;
using Msm.Gui.Features.ShellFeature.ShellDialog.Contract;
using Msm.Gui.Features.ShellFeature.ShellDialog.ViewModels;

namespace Msm.Gui.Features.ShellFeature.ShellDialog.Views
{
    /// <summary>
    ///     Represents the shell dialog window in the WPF application.
    /// </summary>
    internal partial class ShellDialogWindow
        : IShellDialogWindow
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ShellDialogWindow" /> class.
        /// </summary>
        /// <param name="viewModel">The view model for the shell dialog window.</param>
        public ShellDialogWindow(ShellDialogViewModel viewModel)
        {
            InitializeComponent();

            viewModel.SetResult = OnSetResult;

            DataContext = viewModel;
        }

        /// <inheritdoc />
        public Frame GetDialogFrame()
        {
            return FDialogFrame;
        }

        /// <summary>
        ///     Handles the result of the dialog and closes the window.
        /// </summary>
        /// <param name="result">The result of the dialog.</param>
        private void OnSetResult(bool? result)
        {
            DialogResult = result;

            Close();
        }
    }
}
