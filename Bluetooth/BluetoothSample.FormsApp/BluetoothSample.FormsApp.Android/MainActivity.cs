namespace BluetoothSample.FormsApp.Droid
{
    using Acr.UserDialogs;

    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;

    using Smart.Forms.Resolver;
    using Smart.Resolver;

    using BluetoothSample.FormsApp.Components.Dialog;
    using BluetoothSample.FormsApp.Components.Printer;
    using BluetoothSample.FormsApp.Droid.Components.Bluetooth;
    using BluetoothSample.FormsApp.Droid.Components.Dialog;
    using BluetoothSample.FormsApp.Droid.Components.Printer;

    [Activity(
        Name = "bluetoothsample.app.MainActivity",
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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);

            // Components
            UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Boot
            LoadApplication(new App(new ComponentProvider(this)));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
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
                config.Bind<BluetoothHelper>().ToSelf().InSingletonScope();
                config.Bind<IPrinter>().To<DummyPrinter>().InSingletonScope();
            }
        }
    }
}
