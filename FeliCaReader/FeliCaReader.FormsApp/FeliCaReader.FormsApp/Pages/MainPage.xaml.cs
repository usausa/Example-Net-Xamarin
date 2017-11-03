namespace FeliCaReader.FormsApp.Pages
{
    public partial class MainPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
