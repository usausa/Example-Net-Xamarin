namespace Inventory.Client.Pages
{
    using Xamarin.Forms;

    public partial class MenuPage
    {
        public MenuPage(MenuPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
