using System;

namespace Zesty.Core.Common
{
    public static class DateTimeHelper
    {
        public static double GetUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static DateTime GetFromUnixTimestamp(double timestamp)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(timestamp), TimeZoneInfo.Local);
        }
    }
}
