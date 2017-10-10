namespace Inventory.Client.Models.Entity
{
    using Inventory.Client.Helpers;

    using Smart.ComponentModel;

    public class InspectionEntity : NotificationObject
    {
        public const int ItemCodeLength = Length.ItemCode;
        public const int ItemNameLength = Length.ItemName;
        public const int SalesPriceLength = Length.SalesPrice;
        public const int AmountLength = Length.Amount;
        public const int OriginalAmountLength = Length.Amount;
        public const int CrLfLength = Length.CrLf;

        public const int ItemCodeOffset = 0;
        public const int ItemNameOffset = ItemCodeOffset + ItemCodeLength;
        public const int SalesPriceOffset = ItemNameOffset + ItemNameLength;
        public const int AmountOffset = SalesPriceOffset + SalesPriceLength;
        public const int OriginalAmountOffset = AmountOffset + AmountLength;
        public const int CrLfOffset = OriginalAmountOffset + OriginalAmountLength;

        public const int Size = CrLfOffset + CrLfLength;

        private int detailNo;

        private string itemCode;

        private string itemName;

        private long salesPrice;

        private long amount;

        private long originalAmount;

        public int DetailNo
        {
            get { return detailNo; }
            set { SetProperty(ref detailNo, value); }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { SetProperty(ref itemCode, value); }
        }

        public string ItemName
        {
            get { return itemName; }
            set { SetProperty(ref itemName, value); }
        }

        public long SalesPrice
        {
            get { return salesPrice; }
            set { SetProperty(ref salesPrice, value); }
        }

        public long Amount
        {
            get { return amount; }
            set { SetProperty(ref amount, value); }
        }

        public long OriginalAmount
        {
            get { return originalAmount; }
            set { SetProperty(ref originalAmount, value); }
        }

        public void FromBytes(byte[] buffer)
        {
            itemCode = ByteSerializer.ReadString(buffer, ItemCodeOffset, ItemCodeLength);
            itemName = ByteSerializer.ReadString(buffer, ItemNameOffset, ItemNameLength);
            salesPrice = ByteSerializer.ReadLong(buffer, SalesPriceOffset, SalesPriceLength);
            amount = ByteSerializer.ReadLong(buffer, AmountOffset, AmountLength);
            originalAmount = ByteSerializer.ReadLong(buffer, OriginalAmountOffset, OriginalAmountLength);
        }

        public byte[] ToBytes()
        {
            var buffer = new byte[Size];
            ByteSerializer.WriteString(itemCode, buffer, ItemCodeOffset, ItemCodeLength);
            ByteSerializer.WriteString(itemName, buffer, ItemNameOffset, ItemNameLength);
            ByteSerializer.WriteLong(salesPrice, buffer, SalesPriceOffset, SalesPriceLength);
            ByteSerializer.WriteLong(amount, buffer, AmountOffset, AmountLength);
            ByteSerializer.WriteLong(originalAmount, buffer, OriginalAmountOffset, OriginalAmountLength);
            ByteSerializer.WriteCrLf(buffer, CrLfOffset);
            return buffer;
        }
    }
}
