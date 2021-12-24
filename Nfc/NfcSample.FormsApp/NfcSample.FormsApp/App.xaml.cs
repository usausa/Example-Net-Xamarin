namespace NfcSample.FormsApp;

using System.Reflection;

using Smart.Forms.Resolver;
using Smart.Navigation;
using Smart.Resolver;

using NfcSample.FormsApp.Components.Dialog;
using NfcSample.FormsApp.Helpers;
using NfcSample.FormsApp.Modules;
using NfcSample.FormsApp.State;

using XamarinFormsComponents;
using XamarinFormsComponents.Popup;

public partial class App
{
    private readonly SmartResolver resolver;

    private readonly Navigator navigator;

    public App(IComponentProvider provider)
    {
        InitializeComponent();

        // Config Resolver
        resolver = CreateResolver(provider);
        ResolveProvider.Default.UseSmartResolver(resolver);

        // Config Navigator
        navigator = new NavigatorConfig()
            .UseFormsNavigationProvider()
            .UseResolver(resolver)
            .UseIdViewMapper(m => m.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes))
            .ToNavigator();
        navigator.Navigated += (_, args) =>
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

    private SmartResolver CreateResolver(IComponentProvider provider)
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
            adapter.AddJsonSerializer();
            adapter.AddSettings();
        });

        config.BindSingleton<INavigator>(_ => navigator);

        config.BindSingleton<ApplicationState>();

        config.BindSingleton<Configuration>();
        config.BindSingleton<Session>();

        provider.RegisterComponents(config);

        return config.ToResolver();
    }

    protected override async void OnStart()
    {
        var dialogs = resolver.Get<IApplicationDialog>();

        // Crash report
        await CrashReportHelper.ShowReport();

        // Permission
        while (await Permissions.IsPermissionRequired())
        {
            await Permissions.RequestPermissions();

            if (await Permissions.IsPermissionRequired())
            {
                await dialogs.Information("Permission required.");
            }
        }

        // Navigate
        await navigator.ForwardAsync(ViewId.Menu);
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
}
