using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Shorthand for value.ToString().ToLower()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLower(this object value) => value.ToString().ToLower();

        /// <summary>
        /// Shorthand for value.ToString().ToLowerInvariant()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLowerInvariant(this object value) => value.ToString().ToLowerInvariant();

        /// <summary>
        /// Shorthand for !string.IsNullOrEmpty(value)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string value) => !string.IsNullOrEmpty(value);
        public static string StripHtml(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }
        /// <summary>
        /// Shorthand for string.IsNullOrEmpty(value)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// Shorthand for string.Format(value, args);
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string value, params object[] args) => string.Format(value, args);

        /// <summary>
        /// Converts specified string to enum
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">Value to convert</param>
        /// <returns>Enum of T</returns>
        public static T ToEnum<T>(this string value) => ToEnum<T>(value, ignoreCase: false);

        /// <summary>
        /// Converts specified string to enum
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">Value to convert</param>
        /// <param name="ignoreCase">Ignore case</param>
        /// <returns>Enum of T</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// Checks if specified string is valid enum
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">Value to check</param>
        /// <returns>Check result</returns>
        public static bool IsValidEnum<T>(this string value)
            where T : struct => IsValidEnum<T>(value, ignoreCase: false);

        /// <summary>
        /// Checks if specified string is valid enum
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">Value to check</param>
        /// <param name="ignoreCase">Ignore case</param>
        /// <returns>Check result</returns>
        public static bool IsValidEnum<T>(this string value, bool ignoreCase) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Enum.TryParse(value, ignoreCase, out T _);
        }

        /// <summary>
        /// Remove diacritic entries from given text
        /// </summary>
        /// <param name="value"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string value, bool toLower = false)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            string normalizedString = value.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char t in normalizedString)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }
            if (toLower)
                return sb.ToString().ToLower().Normalize(NormalizationForm.FormC);
            else
                return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string EnsureStartsWith(this string value, string prefix)
        {
            return EnsureStartsWith(value, prefix, StringComparison.Ordinal);
        }

        public static string EnsureStartsWith(this string value, string prefix, StringComparison comparisonType)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.StartsWith(prefix, comparisonType))
            {
                return value;
            }

            return prefix + value;
        }

        public static string EnsureStartsWithOrEmpty(this string value, string prefix)
        {
            return EnsureStartsWithOrEmpty(value, prefix, StringComparison.Ordinal);
        }

        public static string EnsureStartsWithOrEmpty(this string value, string prefix, StringComparison comparisonType)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value.StartsWith(prefix, comparisonType))
            {
                return value;
            }

            return prefix + value;
        }

        public static string EnsureEndsWith(this string value, string prefix)
        {
            return EnsureEndsWith(value, prefix, StringComparison.Ordinal);
        }

        public static string EnsureEndsWithOrEmpty(this string value, string prefix)
        {
            return EnsureEndsWithOrEmpty(value, prefix, StringComparison.Ordinal);
        }

        public static string EnsureEndsWith(this string value, string prefix, StringComparison comparisonType)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.EndsWith(prefix, comparisonType))
            {
                return value;
            }

            return value + prefix;
        }

        public static string EnsureEndsWithOrEmpty(this string value, string prefix, StringComparison comparisonType)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value.EndsWith(prefix, comparisonType))
            {
                return value;
            }

            return value + prefix;
        }

        /// <summary>
        /// Assigns <paramref name="replacement"/> value if <paramref name="value"/> is null or empty
        /// </summary>
        /// <param name="value"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string AssignIfNullOrEmpty(this string value, string replacement)
            => value.IsNotNullOrEmpty() ? value : replacement;

        public static string Truncate(this string value, int numberOfChars, string truncationString = "…")
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value.Length > numberOfChars ? value[..numberOfChars] + truncationString : value;
        }
    }
}