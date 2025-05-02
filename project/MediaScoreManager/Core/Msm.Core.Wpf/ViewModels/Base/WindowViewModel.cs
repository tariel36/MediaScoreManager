namespace Msm.Core.Wpf.ViewModels.Base
{
    /// <summary>
    /// Serves as the base class for view models associated with WPF windows, extending the functionality of <see cref="GuiViewModel"/>.
    /// </summary>
    public class WindowViewModel
        : GuiViewModel
    {
        /// <summary>
        /// Gets or sets the title of the window.
        /// </summary>
        public virtual object? WindowTitle { get; set; }
    }
}
