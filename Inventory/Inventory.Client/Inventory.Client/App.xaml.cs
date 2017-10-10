[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]

namespace Inventory.Client
{
    using Inventory.Client.Components;
    using Inventory.Client.Services;

    using Smart.Forms.Components;
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
            navigationService.ForwardAsync("/LoginPage");
        }

        private void RegisterComponents(ResolverConfig config)
        {
            config.UseNavigator();
            config.Bind<ISettingService>().To<SettingService>().InSingletonScope();
            config.Bind<IDialogService>().To<DialogService>().InSingletonScope();
            config.Bind<IPlatformService>().To<PlatformService>().InSingletonScope();
            config.Bind<INetworkClient>().To<NetworkClient>().InSingletonScope();

            config.Bind<ItemService>().ToSelf().InSingletonScope();
            config.Bind<EntryService>().ToSelf().InSingletonScope();
            config.Bind<InspectionService>().ToSelf().InSingletonScope();

            config.Bind<Session>().ToSelf().InSingletonScope();
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
