namespace Inventory.Server.Api.Models
{
    using System;

    public class StorageDetailsResponseEntry
    {
        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public long SalesPrice { get; set; }

        public long Qty { get; set; }
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

        public long TotalQty { get; set; }

        public StorageDetailsResponseEntry[] Entries { get; set; }
    }
}
