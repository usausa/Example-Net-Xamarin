namespace Inventory.Client.Components
{
    using System.Threading.Tasks;

    public interface IBarcodeService
    {
        Task<string> ScanAsync();
    }
}
