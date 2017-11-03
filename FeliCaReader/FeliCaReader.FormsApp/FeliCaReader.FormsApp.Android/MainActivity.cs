namespace FeliCaReader.FormsApp.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Nfc;
    using Android.OS;

    using FeliCaReader.FormsApp.Droid.Services;
    using FeliCaReader.FormsApp.Services;

    using Smart.Resolver;

    [Activity(Label = "FeliCaReader", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { NfcAdapter.ActionTechDiscovered })]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IComponentProvider
    {
        private NfcAdapter nfcDevice;

        private AndroidFeliCaService felicaService;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            var nfcManager = (NfcManager)Application.Context.GetSystemService(NfcService);
            nfcDevice = nfcManager.DefaultAdapter;
            felicaService = new AndroidFeliCaService();

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(this));
        }

        public void RegisterComponents(ResolverConfig config)
        {
            config.Bind<IFeliCaService>().ToConstant(felicaService);
        }

        public override void OnBackPressed()
        {
            MoveTaskToBack(true);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (nfcDevice != null)
            {
                var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
                nfcDevice.EnableForegroundDispatch(
                    this,
                    PendingIntent.GetActivity(this, 0, intent, 0),
                    new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
                    new[] { new[] { "android.nfc.tech.NfcF" } });
            }
        }

        protected override void OnPause()
        {
            base.OnResume();

            nfcDevice?.DisableForegroundDispatch(this);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            felicaService.OnNewIntent(intent);
        }
    }
}
