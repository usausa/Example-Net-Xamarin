namespace Example.FormsApp.Views.Wizard
{
    using Example.FormsApp.Infrastructure;

    using Example.Navigation;

    [Title("Result")]
    [View(ViewId.Result)]
    public partial class ResultView
    {
        public ResultView(ResultViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
