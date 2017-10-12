namespace Inventory.Server.Mvc.Infrastructure
{
    using System;
    using System.Globalization;

    using Microsoft.AspNetCore.Html;

    public static class ViewExtensions
    {
        public static string Then(this bool condition, string value)
        {
            return condition ? value : string.Empty;
        }

        public static HtmlString PadZero(this int value, int length)
        {
            var format = String.Format(CultureInfo.InvariantCulture, "{{0:D{0}}}", length);
            return new HtmlString(String.Format(CultureInfo.InvariantCulture, format, value));
        }

        public static HtmlString PadZero(this int? value, int length)
        {
            return value.HasValue ? value.Value.PadZero(length) : HtmlString.Empty;
        }

        public static HtmlString FormatNumber(this int value)
        {
            return new HtmlString(String.Format(CultureInfo.InvariantCulture, "{0:#,0}", value));
        }

        public static HtmlString FormatNumber(this long value)
        {
            return new HtmlString(String.Format(CultureInfo.InvariantCulture, "{0:#,0}", value));
        }

        public static HtmlString FormatTime(this DateTimeOffset value)
        {
            return new HtmlString(value.ToString("MM/dd HH:mm", CultureInfo.InvariantCulture));
        }

        public static HtmlString FormatTime(this DateTimeOffset? value)
        {
            return value.HasValue ? value.Value.FormatTime() : HtmlString.Empty;
        }
    }
}
