namespace Inventory.Client.Pages.Sync
{
    public partial class EntrySendPage
    {
        public EntrySendPage(EntrySendPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
