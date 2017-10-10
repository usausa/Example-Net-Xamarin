namespace Inventory.Client
{
    using System;
    using System.Globalization;

    public static class Definition
    {
        // Network

        public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(15);

        public static readonly TimeSpan DownloadTimeout = TimeSpan.FromSeconds(180);

        // Item

        public static readonly string ItemFile = "Item.dat";

        // Entry

        public static readonly string EntryDirectory = "Entry";

        public static readonly Func<int, string> EntryFileFormatter = storageNo =>
            String.Format(CultureInfo.InvariantCulture, "{0:D5}.dat", storageNo);

        public static readonly Func<string, int> EntryFileParser = filename =>
            Convert.ToInt32(filename.Substring(0, 5));

        // Inspection

        public static readonly string InspectionDirectory = "Inspection";

        public static readonly Func<int, string> InspectionFileFormatter = storageNo =>
            String.Format(CultureInfo.InvariantCulture, "{0:D5}.dat", storageNo);
    }
}
