namespace NfcSample.FormsApp.Droid;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Acr.UserDialogs;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;

using Smart.Forms.Resolver;
using Smart.Resolver;

using NfcSample.FormsApp.Components.Device;
using NfcSample.FormsApp.Components.Dialog;
using NfcSample.FormsApp.Droid.Components.Device;
using NfcSample.FormsApp.Droid.Components.Dialog;
using NfcSample.FormsApp.Helpers;

using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

[Activity(
    Name = "nfcsample.app.MainActivity",
    Icon = "@mipmap/icon",
    Theme = "@style/MainTheme.Splash",
    MainLauncher = true,
    AlwaysRetainTaskState = true,
    LaunchMode = LaunchMode.SingleInstance,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize,
    ScreenOrientation = ScreenOrientation.Portrait,
    WindowSoftInputMode = SoftInput.AdjustResize)]
public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
{
    [AllowNull]
    private DeviceManager deviceManager;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        SetTheme(Resource.Style.MainTheme);
        base.OnCreate(savedInstanceState);

        // Setup crash report
        //AppDomain.CurrentDomain.UnhandledException += (sender, args) => CrashReport(args.ExceptionObject as Exception);
        TaskScheduler.UnobservedTaskException += (_, args) => CrashReport(args.Exception);
        AndroidEnvironment.UnhandledExceptionRaiser += (_, args) => CrashReport(args.Exception);

        // Service
        deviceManager = new DeviceManager(this);

        // Components
        UserDialogs.Init(this);

        Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        Xamarin.Forms.Forms.Init(this, savedInstanceState);

        // Boot
        LoadApplication(new App(new ComponentProvider(this)));

        // Adjust soft input
        Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
            .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    {
        Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    private static void CrashReport(Exception ex)
    {
        Log.Error("CrashReport", ex.ToString());
        CrashReportHelper.LogException(ex);
    }

    private sealed class ComponentProvider : IComponentProvider
    {
        private readonly MainActivity activity;

        public ComponentProvider(MainActivity activity)
        {
            this.activity = activity;
        }

        public void RegisterComponents(ResolverConfig config)
        {
            config.Bind<Activity>().ToConstant(activity).InSingletonScope();

            config.Bind<IApplicationDialog>().To<ApplicationDialog>().InSingletonScope();
            config.Bind<IDeviceManager>().ToConstant(activity.deviceManager).InSingletonScope();
        }
    }
}
