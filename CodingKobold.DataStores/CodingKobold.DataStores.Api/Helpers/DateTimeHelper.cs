namespace CodingKobold.DataStores.Api.Helpers
{
    public static class DateTimeHelper
    {
        public const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";

        public static string ToSortableString(this DateTime dateTime)
        {
            return dateTime.ToString(DateTimeFormat);
        }
    }
}
