namespace MyShopAPI.Helpers
{
    public static class DateTimeManager
    {
        public static DateTime GetNativeDateTime()
        {
            var localTime = DateTime.Now;
            var safeLocalTime = DateTime.SpecifyKind(localTime, DateTimeKind.Unspecified);
            return safeLocalTime;
        }
    }
}
