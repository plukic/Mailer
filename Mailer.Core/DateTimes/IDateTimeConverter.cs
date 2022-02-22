namespace Mailer.Core.DateTimes
{
    public interface IDateTimeConverter
    {
        DateTime UtcNow { get; }
        DateTime LocalTime { get; }

        DateTime ConvertToLocal(DateTime dateTimeUtc);
        DateTime? ConvertToLocal(DateTime? dateTimeUtc);

        DateTime ConvertToUtc(DateTime dateTimeLocal);
        DateTime? ConvertToUtc(DateTime? dateTimeLocal);

        string ToStringLocal(DateTime dateTimeUtc);
        string ToStringLocal(DateTime? dateTimeUtc);
        string ToStringLocal(DateTime dateTimeUtc, string format);
        string ToStringLocal(DateTime? dateTimeUtc, string format);
        string ToStringUtc(DateTime dateTimeLocal, string format);
        string ToStringUtc(DateTime? dateTimeLocal, string format);
    }
}
