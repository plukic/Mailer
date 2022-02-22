using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Cache
{
    public class CacheSettings
    {
        public static CacheSettings Instance => new();

        /// <summary>
        /// Gets or sets the default cache time in minutes
        /// </summary>
        public int DefaultCacheTime { get; set; } = 60;

        /// <summary>
        /// Gets or sets the short term cache time in minutes
        /// </summary>
        public int ShortTermCacheTime { get; set; } = 3;
    }
}
