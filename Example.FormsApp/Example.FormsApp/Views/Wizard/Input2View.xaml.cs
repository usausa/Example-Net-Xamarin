namespace Example.FormsApp.Views.Wizard
{
    using Example.Navigation;

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
