namespace Inventory.Client.Pages.Edit
{
    public partial class QtyEditPage
    {
        public QtyEditPage(QtyEditPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
