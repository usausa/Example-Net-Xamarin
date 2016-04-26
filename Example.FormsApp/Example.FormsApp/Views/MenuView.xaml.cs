namespace Example.FormsApp.Views
{
    using Example.FormsApp.Infrastructure;

    using Example.Navigation;

    [Title("Menu")]
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
