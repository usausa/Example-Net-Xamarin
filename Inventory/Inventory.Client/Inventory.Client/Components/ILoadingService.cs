namespace Inventory.Client.Components
{
    public interface ILoadingService
    {
        bool Visible { get; }

        void Show(string message);

        void Hide();
    }
}
