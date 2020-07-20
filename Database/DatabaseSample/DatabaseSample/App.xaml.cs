namespace DatabaseSample
{
    using System;
    using System.IO;

    using DatabaseSample.Helpers;
    using DatabaseSample.Services;

    using Smart.Data.Mapper;
    using Smart.Forms.Resolver;
    using Smart.Resolver;

    using Xamarin.Essentials;

    using XamarinFormsComponents;

    public partial class App
    {
        private readonly SmartResolver resolver;

        public App()
        {
            InitializeComponent();

            SqlMapperConfig.Default.ConfigureTypeHandlers(config =>
            {
                config[typeof(DateTime)] = new DateTimeTypeHandler();
            });

            resolver = CreateResolver();
            ResolveProvider.Default.UseSmartResolver(resolver);
        }

        private static SmartResolver CreateResolver()
        {
            var config = new ResolverConfig()
                .UseAutoBinding()
                .UseArrayBinding()
                .UseAssignableBinding()
                .UsePropertyInjector();

            config.UseXamarinFormsComponents(adapter =>
            {
                adapter.AddDialogs();
            });

            config.BindSingleton(new DataServiceOptions
            {
                Path = Path.Combine(FileSystem.AppDataDirectory, "Mobile.db")
            });
            config.BindSingleton<DataService>();

            return config.ToResolver();
        }

        protected override async void OnStart()
        {
            var dataService = resolver.Get<DataService>();
            await dataService.RebuildAsync();

            MainPage = resolver.Get<MainPage>();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
