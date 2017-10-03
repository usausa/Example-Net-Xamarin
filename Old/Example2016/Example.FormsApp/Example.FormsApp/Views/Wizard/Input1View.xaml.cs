namespace Example.FormsApp.Views.Wizard
{
    using Example.FormsApp.Infrastructure;

    using Example.Navigation;

    [Title("Input1")]
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
