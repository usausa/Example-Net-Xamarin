namespace Example.FormsApp.Views.Master
{
    public partial class MasterView
    {
        public MasterView(MasterViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
