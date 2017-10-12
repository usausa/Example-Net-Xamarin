namespace Inventory.Client.Pages.Sync
{
    public partial class InspectionSendPage
    {
        public InspectionSendPage(InspectionSendPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
