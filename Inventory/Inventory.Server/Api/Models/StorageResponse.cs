namespace Inventory.Server.Api.Models
{
    using System;

    public class StorageResponseEntry
    {
        public int StorageNo { get; set; }

        public int EntryUserId { get; set; }

        public DateTimeOffset EntryAt { get; set; }

        public int? InspectionUserId { get; set; }

        public DateTimeOffset? InspectionAt { get; set; }

        public int DetailCount { get; set; }

        public long TotalPrice { get; set; }

        public long TotalQty { get; set; }
    }

    public class StorageResponse
    {
        public StorageResponseEntry[] Entries { get; set; }
    }
}
