namespace Example.FormsApp.Views.Wizard
{
    using Xamarin.Forms;

    public partial class Input1View
    {
        public Input1View(Input1ViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
