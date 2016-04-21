namespace Example.FormsApp.Views.Wizard
{
    using Example.Navigation;

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
