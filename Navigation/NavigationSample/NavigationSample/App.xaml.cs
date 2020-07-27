namespace NavigationSample
{
    using System.Reflection;

    using NavigationSample.Modules;
    using NavigationSample.Services;

    using Smart.Forms.Resolver;
    using Smart.Navigation;
    using Smart.Resolver;

    using XamarinFormsComponents;
    using XamarinFormsComponents.Popup;

    public partial class App
    {
        private readonly Navigator navigator;

        public App()
        {
            InitializeComponent();

            // Config Resolver
            var resolver = CreateResolver();
            ResolveProvider.Default.UseSmartResolver(resolver);

            // Config Navigator
            navigator = new NavigatorConfig()
                .UseFormsNavigationProvider()
                .UseResolver(resolver)
                .UseIdViewMapper(m => m.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes))
                .ToNavigator();
            navigator.Navigated += (sender, args) =>
            {
                // for debug
                System.Diagnostics.Debug.WriteLine(
                    $"Navigated: [{args.Context.FromId}]->[{args.Context.ToId}] : stacked=[{navigator.StackedCount}]");
            };

            // Popup Navigator
            var popupNavigator = resolver.Get<IPopupNavigator>();
            popupNavigator.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes);

            // Show MainWindow
            MainPage = resolver.Get<MainPage>();
        }

        private SmartResolver CreateResolver()
        {
            var config = new ResolverConfig()
                .UseAutoBinding()
                .UseArrayBinding()
                .UseAssignableBinding()
                .UsePropertyInjector()
                .UsePageContextScope();

            config.UseXamarinFormsComponents(adapter =>
            {
                adapter.AddDialogs();
                adapter.AddPopupNavigator();
            });

            config.BindSingleton<INavigator>(kernel => navigator);

            config.BindSingleton<ApplicationState>();

            config.BindSingleton<DataService>();

            return config.ToResolver();
        }

        protected override async void OnStart()
        {
            await navigator.ForwardAsync(ViewId.Menu);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
