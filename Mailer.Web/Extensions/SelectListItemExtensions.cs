using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mailer.Web.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="SelectListItem"/>
    /// </summary>
    public static partial class SelectListItemExtensions
    {
        public static IList<SelectListItem> ToSelectList<T>(this IList<T> list, Func<T, string> textSelector) where T : Enum =>
            list.Select(s => new SelectListItem(textSelector(s), s.ToString())).ToList();

        public static IList<SelectListItem> ToSelectList<T, TValueProperty>(this IList<T> list,
            Func<T, TValueProperty> valueSelector,
            Func<T, string> textSelector)
            where TValueProperty : IConvertible =>
            list.Select(s => new SelectListItem(textSelector(s), valueSelector(s).ToString())).ToList();

        public static IList<SelectListItem> AddOptionItem(this IList<SelectListItem> items, string text, string? value = null, bool markAsSelected = false)
        {
            items.Insert(0, new SelectListItem(text, value ?? string.Empty, markAsSelected));

            return items;
        }

        public static IList<SelectListItem> PreselectItem(this IList<SelectListItem> items, string? value = null)
        {
            var item = items.FirstOrDefault(p => p.Value == value);

            if (item != null)
            {
                item.Selected = true;
            }

            return items;
        }


        public static List<SelectListItem> ToSelectListAsync<TEnum>(this TEnum enumObj,
            Func<TEnum, string> textSelector,
            Func<TEnum, string> valueSelector,
            int[] valuesToExclude = null) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("An Enumeration type is required.", nameof(enumObj));

            return Enum.GetValues(typeof(TEnum)).OfType<TEnum>().Where(enumValue =>
                    valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue)))
                .Select(enumValue => new SelectListItem(textSelector(enumValue), valueSelector(enumValue)))
               .ToList();
        }
    }

}