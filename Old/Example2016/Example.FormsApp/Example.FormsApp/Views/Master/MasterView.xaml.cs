namespace Example.FormsApp.Views.Master
{
    using Example.FormsApp.Infrastructure;

    using Example.Navigation;

    [Title("Master")]
    [View(ViewId.Master)]
    public partial class MasterView
    {
        public MasterView(MasterViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
