namespace Example.FormsApp.Views.Functions
{
    using Example.FormsApp.Infrastructure;

    using Example.Navigation;

    [Title("Calculator")]
    [View(ViewId.Calculator)]
    public partial class CalculatorView
    {
        public CalculatorView(CalculatorViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
