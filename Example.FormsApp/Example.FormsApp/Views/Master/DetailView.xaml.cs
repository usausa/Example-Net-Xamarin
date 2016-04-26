namespace Example.FormsApp.Views.Master
{
    using Example.FormsApp.Infrastructure;

    using Example.Navigation;

    [Title("Detail")]
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
