namespace NF.ExchangeRates.Shared
{
    public static class DateTimeExtensions
    {
        public static DateTime UtcNow
        {
            get
            {
                if (DateTimeProviderContext.Current != null)
                {
                    return DateTimeProviderContext.Current.ContextUtcNow;
                }

                return DateTime.UtcNow;
            }
        }

        public static DateTime Now => UtcNow.ToLocalTime();

        public static long UtcNowUnixTimeSeconds => UtcNow.ToUnixTimeSeconds();

        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            return (uint)new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
        }

        public static DateTime FromUnixTimeSeconds(long unixTimeSeconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeSeconds);
        }

        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeMilliseconds();
        }

        public static DateTime FromUnixTimeMilliseconds(long unixTimeMilliseconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(unixTimeMilliseconds);
        }
    }
}