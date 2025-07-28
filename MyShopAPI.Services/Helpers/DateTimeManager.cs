namespace MyShopAPI.Helpers
{
    public static class DateTimeManager
    {
        public static DateTime ConvertToLocalTime(this DateTime dateTime)
        {
            // Convert UTC to Nigeria time, for example
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Central Africa Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, userTimeZone);

            return localTime;
        }
    }
}
