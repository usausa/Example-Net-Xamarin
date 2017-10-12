namespace Inventory.Server.Models.View
{
    using Inventory.Server.Models.Entity;

    public class StorageView : StorageEntity
    {
        public int DetailCount { get; set; }

        public long TotalPrice { get; set; }

        public long TotalQty { get; set; }
    }
}
