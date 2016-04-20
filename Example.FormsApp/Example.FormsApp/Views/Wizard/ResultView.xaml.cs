namespace Example.FormsApp.Views.Wizard
{
    public partial class ResultView
    {
        public ResultView(ResultViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
