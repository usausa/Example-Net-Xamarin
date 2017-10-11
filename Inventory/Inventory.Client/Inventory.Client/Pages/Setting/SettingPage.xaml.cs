namespace Inventory.Client.Pages.Setting
{
    public partial class SettingPage
    {
        public SettingPage(SettingPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
