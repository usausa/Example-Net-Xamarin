namespace Inventory.Server.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    using Inventory.Server.Models;

    public class StorageDetailsRequestEntry
    {
        [Required]
        [StringLength(Length.ItemCode, MinimumLength = Length.ItemCode)]
        public string ItemCode { get; set; }

        [Required]
        [StringLength(Length.ItemName, MinimumLength = Length.ItemName)]
        public string ItemName { get; set; }

        public long SalesPrice { get; set; }

        public long Qty { get; set; }
    }

    public class StorageDetailsRequest
    {
        public bool Renew { get; set; }

        public int StorageNo { get; set; }

        public int UserId { get; set; }

        [Required]
        public StorageDetailsRequestEntry[] Entries { get; set; }
    }
}
