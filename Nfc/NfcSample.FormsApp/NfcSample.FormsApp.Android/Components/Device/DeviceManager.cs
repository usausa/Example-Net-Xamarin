namespace NfcSample.FormsApp.Droid.Components.Device;

using Android.App;
using Android.Content.PM;

using NfcSample.FormsApp.Components.Device;

public sealed class DeviceManager : DeviceManagerBase
{
    private readonly Activity activity;

    public DeviceManager(Activity activity)
    {
        this.activity = activity;
    }

#pragma warning disable CS0618
    public override void SetOrientation(Orientation orientation)
    {
        var display = activity.WindowManager!.DefaultDisplay!;

        switch (orientation)
        {
            case Orientation.Landscape:
                if (display.Width < display.Height)
                {
                    activity.RequestedOrientation = ScreenOrientation.Landscape;
                }
                break;
            case Orientation.Portrait:
                if (display.Width > display.Height)
                {
                    activity.RequestedOrientation = ScreenOrientation.Portrait;
                }
                break;
        }
    }
#pragma warning restore CS0618

    public override string? GetVersion()
    {
        var pm = activity.PackageManager!;
        var info = pm.GetPackageInfo(activity.PackageName!, 0)!;
        return info.VersionName;
    }
}
