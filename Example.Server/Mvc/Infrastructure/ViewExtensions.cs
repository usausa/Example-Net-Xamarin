namespace Example.Server.Mvc.Infrastructure
{
    using System;
    using System.Globalization;

    public static class ViewExtensions
    {
        public static string PadZero(this int value, int length)
        {
            var format = String.Format(CultureInfo.InvariantCulture, "{{0:D{0}}}", length);
            return String.Format(CultureInfo.InvariantCulture, format, value);
        }

        public static string PadZero(this int? value, int length)
        {
            return value.HasValue ? value.Value.PadZero(length) : string.Empty;
        }

        public static string FormatNumber(this int value)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:#,0}", value);
        }

        public static string FormatNumber(this long value)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:#,0}", value);
        }

        public static string FormatTime(this DateTimeOffset value)
        {
            return value.ToString("MM/dd HH:mm", CultureInfo.InvariantCulture);
        }

        public static string FormatTime(this DateTimeOffset? value)
        {
            return value.HasValue ? value.Value.FormatTime() : string.Empty;
        }
    }
}
