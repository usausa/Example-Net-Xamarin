namespace Inventory.Client.Pages
{
    public partial class MenuPage
    {
        public MenuPage(MenuPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
