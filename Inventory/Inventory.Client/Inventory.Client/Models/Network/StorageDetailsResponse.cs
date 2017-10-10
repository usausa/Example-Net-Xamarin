namespace Inventory.Client.Models.Network
{
    using System;

    public class StorageDetailsResponseEntry
    {
        public int DetailNo { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public long SalesPrice { get; set; }

        public long Amount { get; set; }
    }

    public class StorageDetailsResponse
    {
        public int StorageNo { get; set; }

        public int EntryUserId { get; set; }

        public DateTimeOffset EntryAt { get; set; }

        public int? InspectionUserId { get; set; }

        public DateTimeOffset? InspectionAt { get; set; }

        public int DetailCount { get; set; }

        public long TotalPrice { get; set; }

        public long TotalAmount { get; set; }

        public StorageDetailsResponseEntry[] Entries { get; set; }
    }
}
