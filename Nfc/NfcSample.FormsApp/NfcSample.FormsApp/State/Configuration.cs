namespace NfcSample.FormsApp.State;

using Xamarin.Essentials;

public class Configuration
{
    public string ApiKey
    {
        get => Preferences.Get(nameof(ApiKey), string.Empty);
        set => Preferences.Set(nameof(ApiKey), value);
    }

    public string ApiEndPoint
    {
        get => Preferences.Get(nameof(ApiEndPoint), string.Empty);
        set => Preferences.Set(nameof(ApiEndPoint), value);
    }
}
