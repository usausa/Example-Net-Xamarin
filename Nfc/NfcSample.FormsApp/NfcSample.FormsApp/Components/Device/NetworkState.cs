namespace NfcSample.FormsApp.Components.Device;

public enum NetworkState
{
    Connected,
    ConnectedHighSpeed,
    Disconnected
}

public static class NetworkStateExtensions
{
    public static bool IsConnected(this NetworkState state)
    {
        return state == NetworkState.Connected || state == NetworkState.ConnectedHighSpeed;
    }
}
