namespace Example.Server.Models.Entity
{
    public class StorageDetailEntity
    {
        public int StorageNo { get; set; }

        public int DetailNo { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public long SalesPrice { get; set; }

        public long Amount { get; set; }
    }
}
