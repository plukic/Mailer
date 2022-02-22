using Mailer.Core.Timezones;
using System.Globalization;

namespace Mailer.Core.DateTimes
{
    public class DateTimeConverter : IDateTimeConverter
    {

        private readonly ITimezoneService _timezoneService;

        public DateTimeConverter(ITimezoneService timezoneService)
        {
            _timezoneService = timezoneService;
        }

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime LocalTime => ConvertToLocal(UtcNow);

        public DateTime ConvertToLocal(DateTime dateTimeUtc)
        {
            var applicationTimezone = _timezoneService.GetCurrentTimezoneInfo();
            return TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, applicationTimezone);
        }

        public DateTime? ConvertToLocal(DateTime? dateTimeUtc)
        {
            if (!dateTimeUtc.HasValue)
                return null;
            return ConvertToLocal(dateTimeUtc.Value);
        }

        public DateTime ConvertToUtc(DateTime dateTimeLocal)
        {
            var applicationTimezone = _timezoneService.GetCurrentTimezoneInfo();
            return TimeZoneInfo.ConvertTimeToUtc(dateTimeLocal, applicationTimezone);
        }
        public DateTime? ConvertToUtc(DateTime? dateTimeLocal)
        {
            if (!dateTimeLocal.HasValue)
                return null;
            return ConvertToUtc(dateTimeLocal.Value);
        }

        public string ToStringLocal(DateTime dateTimeUtc)
        {
            return dateTimeUtc == default ? "" : ConvertToLocal(dateTimeUtc).ToString();
        }

        public string ToStringLocal(DateTime? dateTimeUtc)
        {
            return !dateTimeUtc.HasValue || dateTimeUtc.Value == default ?
                "" :
                ConvertToLocal(dateTimeUtc.Value).ToString();
        }

        public string ToStringLocal(DateTime dateTimeUtc, string format)
        {
            return dateTimeUtc == default ?
                   "" : ConvertToLocal(dateTimeUtc).ToString(format);
        }

        public string ToStringLocal(DateTime? dateTimeUtc, string format)
        {
            return !dateTimeUtc.HasValue || dateTimeUtc.Value == default ?
                "" :
                ConvertToLocal(dateTimeUtc.Value).ToString(format);
        }


        public string ToStringUtc(DateTime dateTimeLocal, string format)
        {
            return dateTimeLocal == default ?
                "" :
                ConvertToUtc(dateTimeLocal).ToString(format, CultureInfo.InvariantCulture);
        }

        public string ToStringUtc(DateTime? dateTimeLocal, string format)
        {
            return !dateTimeLocal.HasValue || dateTimeLocal.Value == default ?
                "" :
                ConvertToUtc(dateTimeLocal.Value).ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
