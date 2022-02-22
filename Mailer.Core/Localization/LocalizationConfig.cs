using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Localization
{
    /// <summary>
    /// Defines localization configuration
    /// </summary>
    public class LocalizationConfig
    {
        /// <summary>
        /// Contains languages available for application
        /// </summary>
        private readonly static IReadOnlyCollection<Language> _supportedLanguages = new[]
        {
            new Language("English", "en", "en.png", isDefault: true),
        };

        public static IReadOnlyCollection<Language> SupportedLanguages => _supportedLanguages;

        public static Language GetDefaultLanguage() => SupportedLanguages.FirstOrDefault(p => p.IsDefault);

        /// <summary>
        /// Returns supported languages in culture-info form
        /// </summary>
        public static IReadOnlyCollection<CultureInfo> GetSupportedCultures()
        {
            return SupportedLanguages.Select(s => new CultureInfo(s.Code)).ToList();
        }

        public static CultureInfo GetDefaultCulture() => new(GetDefaultLanguage().Code);
    }
}
