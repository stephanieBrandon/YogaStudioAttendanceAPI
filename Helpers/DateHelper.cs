namespace YogaStudioAttendanceAPI.Helpers
{
    /// <summary>
    /// Provides datetime values converted to Eastern Time regardless of server timezone.
    /// 
    /// WHY: Render deploys our API on UTC servers, but all our users are in Ontario.
    /// DateTime.Now returns the server's timezone, so on Render it shows UTC (4-5 hours
    /// ahead of us). Using DateHelper.Now/Today ensures Eastern Time is stored in the DB.
    /// 
    /// Use this helper instead of DateTime.Now and DateTime.Today throughout the API.
    /// </summary>
    public static class DateHelper
    {
        // America/Toronto handles Eastern Time including daylight saving transitions
        private static readonly TimeZoneInfo EasternTime =
            TimeZoneInfo.FindSystemTimeZoneById("America/Toronto");

        // Current Eastern Time datetime (replacement for DateTime.Now)
        public static DateTime Now => TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow, EasternTime);

        // Today's date in Eastern Time (replacement for DateTime.Today)
        public static DateTime Today => Now.Date;
    }
}