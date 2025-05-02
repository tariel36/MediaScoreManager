using System;

namespace Msm.Gui.View.Pages.Models
{
    /// <summary>
    /// Represents metadata for a page, including its associated view model and page type.
    /// </summary>
    internal class PageMetaData
    {
        /// <summary>
        /// Gets or sets the key associated with the page.
        /// </summary>
        public required string? Key { get; init; }

        /// <summary>
        /// Gets or sets the indicator whether the page is considered main view or not.
        /// </summary>
        public required bool IsMain { get; init; }

        /// <summary>
        /// Gets or sets the type of the view model associated with the page.
        /// </summary>
        public required Type ViewModel { get; init; }

        /// <summary>
        /// Gets or sets the type of the page.
        /// </summary>
        public required Type Page { get; init; }

        /// <summary>
        /// Creates a new instance of <see cref="PageMetaData"/> with the specified view model and page types.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <typeparam name="TPage">The type of the page.</typeparam>
        /// <returns>A new instance of <see cref="PageMetaData"/>.</returns>
        public static PageMetaData Create<TViewModel, TPage>(string? key, bool isMain)
        {
            return new()
            {
                Key = key,
                IsMain = isMain,
                ViewModel = typeof(TViewModel),
                Page = typeof(TPage)
            };
        }
    }
}
