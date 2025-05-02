using System.ComponentModel;

namespace Msm.Core.Wpf.ViewModels.Base
{
    /// <summary>
    /// Serves as the base class for GUI-related view models in the WPF application, providing property change notification functionality and design-time detection.
    /// </summary>
    public abstract class GuiViewModel
        : BaseViewModel
    {
        /// <summary>
        /// Gets a value indicating whether the application is running in design mode.
        /// </summary>
        public static bool IsDesignerMode
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }
    }
}
