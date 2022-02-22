using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Timezones
{
    public interface ITimezoneService
    {
        TimeZoneInfo GetCurrentTimezoneInfo();
    }
}
