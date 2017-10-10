namespace Inventory.Client.Models.Entity
{
    using Inventory.Client.Helpers;

    public class ItemEntity
    {
        public const int ItemCodeLength = Length.ItemCode;
        public const int ItemNameLength = Length.ItemName;
        public const int SalesPriceLength = Length.SalesPrice;
        public const int CrLfLength = Length.CrLf;

        public const int ItemCodeOffset = 0;
        public const int ItemNameOffset = ItemCodeOffset + ItemCodeLength;
        public const int SalesPriceOffset = ItemNameOffset + ItemNameLength;
        public const int CrLfOffset = SalesPriceOffset + SalesPriceLength;

        public const int Size = CrLfOffset + CrLfLength;

        public string ItemCode { get; }

        public string ItemName { get; }

        public long SalesPrice { get; }

        public ItemEntity(byte[] buffer)
        {
            ItemCode = ByteSerializer.ReadString(buffer, ItemCodeOffset, ItemCodeLength);
            ItemName = ByteSerializer.ReadString(buffer, ItemNameOffset, ItemNameLength);
            SalesPrice = ByteSerializer.ReadLong(buffer, SalesPriceOffset, SalesPriceLength);
        }
    }
}
