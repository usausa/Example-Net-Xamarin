namespace Inventory.Server.Models.Entity
{
    using System;

    public class StorageEntity
    {
        public int StorageNo { get; set; }

        public int EntryUserId { get; set; }

        public DateTimeOffset EntryAt { get; set; }

        public int? InspectionUserId { get; set; }

        public DateTimeOffset? InspectionAt { get; set; }
    }
}
