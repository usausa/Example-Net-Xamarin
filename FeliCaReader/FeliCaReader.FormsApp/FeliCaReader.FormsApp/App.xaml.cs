[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]

namespace FeliCaReader.FormsApp
{
    using Smart.Forms.Navigation;
    using Smart.Resolver;

    using Xamarin.Forms;

    public partial class App
    {
        private IResolver Resolver { get; }

        public App(IComponentProvider provider)
        {
            InitializeComponent();

            var config = new ResolverConfig();
            RegisterComponents(config);
            provider.RegisterComponents(config);
            Resolver = config.ToResolver();

            MainPage = new NavigationPage { BarBackgroundColor = (Color)Resources["MetroBlueDark"] };

            var navigationService = Resolver.Get<INavigator>();
            navigationService.ForwardAsync("/MainPage");
        }

        private void RegisterComponents(ResolverConfig config)
        {
            config.UseAutoBinding();
            config.UseNavigator();
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
