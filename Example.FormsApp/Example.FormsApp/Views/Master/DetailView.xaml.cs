namespace Example.FormsApp.Views.Master
{
    using Example.Navigation;

    [View(ViewId.DetailNew)]
    [View(ViewId.DetailEdit)]
    public partial class DetailView
    {
        public DetailView(DetailViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
