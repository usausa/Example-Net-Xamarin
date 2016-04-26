namespace Example.FormsApp.Views.Wizard
{
    using Example.FormsApp.Infrastructure;

    using Example.Navigation;

    [Title("Input2")]
    [View(ViewId.Input2)]
    public partial class Input2View
    {
        public Input2View(Input2ViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
