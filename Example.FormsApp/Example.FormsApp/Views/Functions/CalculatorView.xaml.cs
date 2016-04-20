namespace Example.FormsApp.Views.Functions
{
    public partial class CalculatorView
    {
        public CalculatorView(CalculatorViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
