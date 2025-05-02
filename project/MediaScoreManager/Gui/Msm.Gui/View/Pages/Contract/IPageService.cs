using System;
using System.Windows.Controls;

namespace Msm.Gui.View.Pages.Contract
{
    /// <summary>
    /// Defines a contract for managing pages in a WPF application.
    /// </summary>
    internal interface IPageService
    {
        /// <summary>
        /// Retrieves the type of the page associated with the specified key.
        /// </summary>
        /// <param name="key">The key identifying the page.</param>
        /// <returns>The <see cref="Type"/> of the page.</returns>
        Type GetPageType(string key);

        /// <summary>
        /// Retrieves an instance of the page associated with the specified key.
        /// </summary>
        /// <param name="key">The key identifying the page.</param>
        /// <returns>An instance of the <see cref="Page"/>, or <see langword="null"/> if not found.</returns>
        Page? GetPage(string key);

        /// <summary>
        /// Retrieves an instance of the main page.
        /// </summary>
        /// <returns>An instance of the <see cref="Page"/> that is considered the main page.</returns>
        /// <exception cref="InvalidOperationException">If more than one main page exists or no main page exists at all.</exception>
        Page? GetMainPage();
    }
}
