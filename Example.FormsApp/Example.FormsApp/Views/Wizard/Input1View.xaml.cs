namespace Example.FormsApp.Views.Wizard
{
    using Example.Navigation;

    [View(ViewId.Input1)]
    public partial class Input1View
    {
        public Input1View(Input1ViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
