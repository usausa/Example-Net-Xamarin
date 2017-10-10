namespace Inventory.Client.Models
{
    using System;
    using System.Globalization;

    public static class Format
    {
        public static readonly string UserIdFormat
            = String.Format(CultureInfo.InvariantCulture, "{{0:D{0}}}", Length.UserId);

        public static readonly string StorageNoFormat
            = String.Format(CultureInfo.InvariantCulture, "{{0:D{0}}}", Length.StorageNo);

        public static readonly string DetailNoFormat
            = String.Format(CultureInfo.InvariantCulture, "{{0:D{0}}}", Length.DetailNo);

        public static string UserId => UserIdFormat;

        public static string StorageNo => StorageNoFormat;

        public static string DetailNo => DetailNoFormat;
    }
}
