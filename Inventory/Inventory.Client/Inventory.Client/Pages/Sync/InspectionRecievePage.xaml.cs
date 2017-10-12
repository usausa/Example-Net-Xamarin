namespace Inventory.Client.Pages.Sync
{
    public partial class InspectionRecievePage
    {
        public InspectionRecievePage(InspectionRecievePageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
