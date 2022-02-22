using Mailer.Core.Base;


namespace Mailer.Core.Localization
{
    /// <summary>
    /// Represents entity translation per field
    /// </summary>
    public class LocalizedProperty : Entity
    {
        public int EntityId { get; set; }
        public int LanguageId { get; set; }
        public string LocaleKeyGroup { get; set; }
        public string LocaleKey { get; set; }
        public string LocaleValue { get; set; }

        public Language Language { get; set; }
    }
}
