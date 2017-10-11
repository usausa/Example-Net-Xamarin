namespace Inventory.Client.Pages
{
    using Xamarin.Forms;

    public partial class LoginPage
    {
        public LoginPage(LoginPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
