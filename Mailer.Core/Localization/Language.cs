using Mailer.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Localization
{
    /// <summary>
    /// This model will not be mapped to database since we do not use db localization.
    /// For now it is used for
    /// </summary>
    public class Language : Entity
    {
        public Language()
        {
            LocalizedProperties = new HashSet<LocalizedProperty>();
        }

        public Language(string name, string code, string iconImageName, bool isDefault)
            : this()
        {
            Name = name;
            Code = code;
            IconImageName = iconImageName;
            IsDefault = isDefault;
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string IconImageName { get; set; }
        public bool IsDefault { get; set; }

        public ICollection<LocalizedProperty> LocalizedProperties { get; set; }
    }

    public static class LanguageConst
    {
        public const int MaxNameLen = 100;
        public const int MaxCodeLen = 15;
        public const int MaxIconImageNameLen = 50;
    }
}
