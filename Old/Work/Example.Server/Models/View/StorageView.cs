namespace Example.Server.Models.View
{
    using Example.Server.Models.Entity;

    public class StorageView : StorageEntity
    {
        public int DetailCount { get; set; }

        public long TotalPrice { get; set; }

        public long TotalAmount { get; set; }
    }
}
