namespace Example.FormsApp.Views
{
    public partial class MenuView
    {
        public MenuView(MenuViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
