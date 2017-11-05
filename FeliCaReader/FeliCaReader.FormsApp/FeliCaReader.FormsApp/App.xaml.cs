[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]

namespace FeliCaReader.FormsApp
{
    using FeliCaReader.FormsApp.Pages;
    using FeliCaReader.FormsApp.Services;

    public partial class App
    {
        public App(IFeliCaService feliCaService)
        {
            InitializeComponent();

            MainPage = new MainPage(new MainPageViewModel(feliCaService));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
