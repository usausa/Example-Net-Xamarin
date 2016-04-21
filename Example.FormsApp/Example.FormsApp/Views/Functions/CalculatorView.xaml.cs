namespace Example.FormsApp.Views.Functions
{
    using Example.Navigation;

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
