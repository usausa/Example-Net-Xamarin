namespace Example.FormsApp.Views.Master
{
    using Example.Navigation;

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
