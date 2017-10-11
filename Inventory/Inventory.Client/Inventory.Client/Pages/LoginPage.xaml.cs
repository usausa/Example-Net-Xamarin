namespace Inventory.Client.Pages
{
    public partial class LoginPage
    {
        public LoginPage(LoginPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
