namespace Example.FormsApp
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    /// <summary>
    ///
    /// </summary>
    [Activity(
        Label = "Example.FormsApp",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidDevice()));
        }
    }
}
