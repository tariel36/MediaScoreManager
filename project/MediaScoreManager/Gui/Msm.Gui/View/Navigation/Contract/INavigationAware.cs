namespace Msm.Gui.View.Navigation.Contract
{
    /// <summary>
    /// Defines a contract for handling navigation events in a WPF application.
    /// </summary>
    internal interface INavigationAware
    {
        /// <summary>
        /// Called when the object is navigated to.
        /// </summary>
        /// <param name="parameter">An optional parameter passed during navigation.</param>
        void OnNavigatedTo(object? parameter);

        /// <summary>
        /// Called when the object is navigated away from.
        /// </summary>
        void OnNavigatedFrom();
    }
}
