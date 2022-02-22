using Mailer.Core.Timezones.FromConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Timezones
{
    public class TimezoneFromConfigurationProvider : ITimezoneService
    {
        private readonly TimeZoneInfo localTimezone;
        
        public TimezoneFromConfigurationProvider(TimezoneConfiguration timezoneConfiguration)
        {
            localTimezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneConfiguration.Id);
        }
        public TimeZoneInfo GetCurrentTimezoneInfo()
        {
            return localTimezone;
        }
    }
}
