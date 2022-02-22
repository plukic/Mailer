namespace System
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Transforms enum name to localized key, basically combines enum type + value
        /// </summary>
        /// <returns></returns>
        public static string ToLocalizationKey<TEnum>(this TEnum value) where TEnum : Enum
        {
            if (value != null)
            {
                return typeof(TEnum).Name + value.ToString();
            }

            return string.Empty;
        }

        public static string ToLocalizationKey<TEnum>(this TEnum? value, string fallbackKey) where TEnum : struct, Enum
            => value != null ? typeof(TEnum).Name + value.ToString() : fallbackKey;
    }
}
