namespace Inventory.Client.Models.Network
{
    public class StorageDetailsRequestEntry
    {
        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public long SalesPrice { get; set; }

        public long Qty { get; set; }
    }

    public class StorageDetailsRequest
    {
        public bool Renew { get; set; }

        public int StorageNo { get; set; }

        public int UserId { get; set; }

        public StorageDetailsRequestEntry[] Entries { get; set; }
    }
}
