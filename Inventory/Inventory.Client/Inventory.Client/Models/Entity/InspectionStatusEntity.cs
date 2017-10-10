namespace Inventory.Client.Models.Entity
{
    using System;

    using Inventory.Client.Helpers;

    using Smart.ComponentModel;

    public class InspectionStatusEntity : NotificationObject
    {
        public const int StorageNoLength = Length.StorageNo;
        public const int EntryUserIdLength = Length.UserId;
        public const int EntryAtLength = Length.DateTime;
        public const int InspectionUserIdLength = Length.UserId;
        public const int InspectionAtLength = Length.DateTime;
        public const int DetailCountLength = Length.DetailNo;
        public const int TotalPriceLength = Length.TotalPrice;
        public const int TotalAmountLength = Length.TotalAmount;
        public const int IsCheckedLength = Length.Flag;
        public const int CrLfLength = Length.CrLf;

        public const int StorageNoOffset = 0;
        public const int EntryUserIdOffset = StorageNoOffset + StorageNoLength;
        public const int EntryAtOffset = EntryUserIdOffset + EntryUserIdLength;
        public const int InspectionUserIdOffset = EntryAtOffset + EntryAtLength;
        public const int InspectionAtOffset = InspectionUserIdOffset + InspectionUserIdLength;
        public const int DetailCountOffset = InspectionAtOffset + InspectionAtLength;
        public const int TotalPriceOffset = DetailCountOffset + DetailCountLength;
        public const int TotalAmountOffset = TotalPriceOffset + TotalPriceLength;
        public const int IsCheckedOffset = TotalAmountOffset + TotalAmountLength;
        public const int CrLfOffset = IsCheckedOffset + IsCheckedLength;

        public const int Size = CrLfOffset + CrLfLength;

        private int storageNo;

        private int entryUserId;

        private DateTimeOffset entryAt;

        private int? inspectionUserId;

        private DateTimeOffset? inspectionAt;

        private int detailCount;

        private long totalPrice;

        private long totalAmount;

        private bool isChecked;

        public int StorageNo
        {
            get => storageNo;
            set => SetProperty(ref storageNo, value);
        }

        public int EntryUserId
        {
            get => entryUserId;
            set => SetProperty(ref entryUserId, value);
        }

        public DateTimeOffset EntryAt
        {
            get => entryAt;
            set => SetProperty(ref entryAt, value);
        }

        public int? InspectionUserId
        {
            get => inspectionUserId;
            set => SetProperty(ref inspectionUserId, value);
        }

        public DateTimeOffset? InspectionAt
        {
            get => inspectionAt;
            set => SetProperty(ref inspectionAt, value);
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

        public long TotalAmount
        {
            get => totalAmount;
            set => SetProperty(ref totalAmount, value);
        }

        public bool IsChecked
        {
            get => isChecked;
            set => SetProperty(ref isChecked, value);
        }

        public void FromBytes(byte[] buffer)
        {
            storageNo = ByteSerializer.ReadInteger(buffer, StorageNoOffset, StorageNoLength);
            entryUserId = ByteSerializer.ReadInteger(buffer, EntryUserIdOffset, EntryUserIdLength);
            entryAt = ByteSerializer.ReadDateTime(buffer, EntryAtOffset, EntryAtLength);
            inspectionUserId = ByteSerializer.ReadIntegerNullable(buffer, InspectionUserIdOffset, InspectionUserIdLength);
            inspectionAt = ByteSerializer.ReadDateTimeNullable(buffer, InspectionAtOffset, InspectionAtLength);
            detailCount = ByteSerializer.ReadInteger(buffer, DetailCountOffset, DetailCountLength);
            totalPrice = ByteSerializer.ReadLong(buffer, TotalPriceOffset, TotalPriceLength);
            totalAmount = ByteSerializer.ReadLong(buffer, TotalAmountOffset, TotalAmountLength);
            isChecked = ByteSerializer.ReadBoolean(buffer, IsCheckedOffset, IsCheckedLength);
        }

        public byte[] ToBytes()
        {
            var buffer = new byte[Size];
            ByteSerializer.WriteInteger(storageNo, buffer, StorageNoOffset, StorageNoLength);
            ByteSerializer.WriteInteger(entryUserId, buffer, EntryUserIdOffset, EntryUserIdLength);
            ByteSerializer.WriteDateTime(entryAt, buffer, EntryAtOffset, EntryAtLength);
            ByteSerializer.WriteIntegerNullable(inspectionUserId, buffer, InspectionUserIdOffset, InspectionUserIdLength);
            ByteSerializer.WriteDateTimeNullable(inspectionAt, buffer, InspectionAtOffset, InspectionAtLength);
            ByteSerializer.WriteInteger(detailCount, buffer, DetailCountOffset, DetailCountLength);
            ByteSerializer.WriteLong(totalPrice, buffer, TotalPriceOffset, TotalPriceLength);
            ByteSerializer.WriteLong(totalAmount, buffer, TotalAmountOffset, TotalAmountLength);
            ByteSerializer.WriteBoolean(isChecked, buffer, IsCheckedOffset, IsCheckedLength);
            ByteSerializer.WriteCrLf(buffer, CrLfOffset);
            return buffer;
        }
    }
}
