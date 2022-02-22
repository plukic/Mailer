using Mailer.Core.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
namespace Mailer.Web.Views.Shared
{
    /// <summary>
    /// Extension methods for <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> used inside Razor views/pages.
    /// This way we avoid dynamic properties with ViewBag and magic string used with ViewData
    /// </summary>
    public static partial class ViewDataDictionaryLayoutExtensions
    {
        /// <summary>
        /// Prefix for all view data keys
        /// </summary>
        private const string Prefix = "Layout_";

        private const string Title = Prefix + nameof(Title);
        private const string TitleIcon = Prefix + nameof(TitleIcon);
        private const string ActiveNavMenuItem = Prefix + nameof(ActiveNavMenuItem);

        /// <summary>
        /// Sets currently active menu items
        /// All items will be joined as "item1.item2" etc.
        /// </summary>
        /// <param name="activeItems">Set of currently active menu items, ie. Settings, Users</param>
        public static void SetActiveNavMenuItem(this ViewDataDictionary viewData, params string[] activeItems)
        {
            if (activeItems != null && activeItems.Any())
            {
                viewData[ActiveNavMenuItem] = activeItems.JoinAsString(".");
            }
        }

        /// <summary>
        /// Sets title for current page
        /// </summary>
        public static void SetPageTitle(this ViewDataDictionary viewData, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                viewData[Title] = value;
            }
        }

        /// <summary>
        /// Sets title for current page from multiple segments
        /// </summary>
        public static void SetPageTitle(this ViewDataDictionary viewData, params string[] values)
        {
            if (values.Any())
            {
                viewData[Title] = string.Join(" - ", values);
            }
        }

        /// <summary>
        /// Sets title icon for current page
        /// </summary>
        public static void SetPageTitleIcon(this ViewDataDictionary viewData, string path)
        {
            if (path.IsNotNullOrEmpty())
            {
                viewData[TitleIcon] = path;
            }
        }

        /// <summary>
        /// Returns page title icon
        /// </summary>
        public static string GetPageTitleIcon(this ViewDataDictionary viewData) => viewData[TitleIcon] as string;

        /// <summary>
        /// Sets title for current page
        /// </summary>
        public static void SetPageTitle(this ViewDataDictionary viewData, LocalizedString localizedString)
        {
            if (localizedString != null)
            {
                viewData[Title] = localizedString.ToString();
            }
        }

        /// <summary>
        /// Returns page title
        /// </summary>
        public static string GetPageTitle(this ViewDataDictionary viewData) => viewData[Title] as string;

        /// <summary>
        /// Returns active nav menu item
        /// </summary>
        public static string GetActiveNavMenuItem(this ViewDataDictionary viewData) => viewData[ActiveNavMenuItem] as string;
    }
}
