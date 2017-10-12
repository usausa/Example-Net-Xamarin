namespace Inventory.Client.Pages.Sync
{
    public partial class MasterDownloadPage
    {
        public MasterDownloadPage(MasterDownloadPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
