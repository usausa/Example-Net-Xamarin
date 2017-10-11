namespace Inventory.Client.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using Inventory.Client.Components;
    using Inventory.Client.Droid.Components;

    using Smart.Resolver;

    using ZXing.Mobile;

    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            MobileBarcodeScanner.Initialize(Application);

            LoadApplication(new App(new ComponentProvider()));
        }

        public override void OnBackPressed()
        {
            MoveTaskToBack(true);
        }

        private class ComponentProvider : IComponentProvider
        {
            public void RegisterComponents(ResolverConfig config)
            {
                config.Bind<IBarcodeService>().To<MobileBarcodeService>().InSingletonScope();
                config.Bind<IFileService>().To<FileService>().InSingletonScope();
                config.Bind<ILoadingService>().To<LoadingService>().InSingletonScope();
            }
        }
    }
}
