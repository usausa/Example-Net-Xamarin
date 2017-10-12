namespace Inventory.Client.Models.Entity
{
    using Inventory.Client.Helpers;

    using Smart.ComponentModel;

    public class EntryStatusEntity : NotificationObject
    {
        public const int StorageNoLength = Length.StorageNo;
        public const int DetailCountLength = Length.DetailNo;
        public const int TotalPriceLength = Length.TotalPrice;
        public const int TotalQtyLength = Length.TotalQty;
        public const int CrLfLength = Length.CrLf;

        public const int StorageNoOffset = 0;
        public const int DetailCountOffset = StorageNoOffset + StorageNoLength;
        public const int TotalPriceOffset = DetailCountOffset + DetailCountLength;
        public const int TotalQtyOffset = TotalPriceOffset + TotalPriceLength;
        public const int CrLfOffset = TotalQtyOffset + TotalQtyLength;

        public const int Size = CrLfOffset + CrLfLength;

        private int storageNo;

        private int detailCount;

        private long totalPrice;

        private long totalQty;

        public int StorageNo
        {
            get => storageNo;
            set => SetProperty(ref storageNo, value);
        }

        public int DetailCount
        {
            get => detailCount;
            set => SetProperty(ref detailCount, value);
        }

        public long TotalPrice
        {
            get => totalPrice;
            set => SetProperty(ref totalPrice, value);
        }

        public long TotalQty
        {
            get => totalQty;
            set => SetProperty(ref totalQty, value);
        }

        public void FromBytes(byte[] buffer)
        {
            storageNo = ByteSerializer.ReadInteger(buffer, StorageNoOffset, StorageNoLength);
            detailCount = ByteSerializer.ReadInteger(buffer, DetailCountOffset, DetailCountLength);
            totalPrice = ByteSerializer.ReadLong(buffer, TotalPriceOffset, TotalPriceLength);
            totalQty = ByteSerializer.ReadLong(buffer, TotalQtyOffset, TotalQtyLength);
        }

        public byte[] ToBytes()
        {
            var buffer = new byte[Size];
            ByteSerializer.WriteInteger(storageNo, buffer, StorageNoOffset, StorageNoLength);
            ByteSerializer.WriteInteger(detailCount, buffer, DetailCountOffset, DetailCountLength);
            ByteSerializer.WriteLong(totalPrice, buffer, TotalPriceOffset, TotalPriceLength);
            ByteSerializer.WriteLong(totalQty, buffer, TotalQtyOffset, TotalQtyLength);
            ByteSerializer.WriteCrLf(buffer, CrLfOffset);
            return buffer;
        }
    }
}
