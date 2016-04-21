namespace Example.FormsApp.Views
{
    using Example.Navigation;

    [View(ViewId.Menu)]
    public partial class MenuView
    {
        public MenuView(MenuViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
