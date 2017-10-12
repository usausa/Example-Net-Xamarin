namespace Inventory.Client.Models
{
    public static class Length
    {
        public const int TerminalNo = 6;

        public const int UserId = 6;

        public const int StorageNo = 5;

        public const int DetailNo = 4;

        public const int ItemCode = 20;
        public const int ItemName = 20;
        public const int SalesPrice = 7;
        public const int Qty = 3;

        public const int TotalPrice = DetailNo + Qty + SalesPrice;
        public const int TotalQty = DetailNo + Qty;

        public const int DateTime = 17;
        public const int Flag = 1;

        public const int CrLf = 2;
    }
}
