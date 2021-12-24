namespace NfcSample.FormsApp.Components.Device;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

using Xamarin.Essentials;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ignore")]
public abstract class DeviceManagerBase : IDeviceManager
{
    private readonly BehaviorSubject<NetworkState> networkState;

    public IObservable<NetworkState> NetworkState => networkState;

    protected DeviceManagerBase()
    {
        networkState = new BehaviorSubject<NetworkState>(GetNetworkState(Connectivity.NetworkAccess, Connectivity.ConnectionProfiles));
        Connectivity.ConnectivityChanged += (_, args) =>
        {
            networkState.OnNext(GetNetworkState(args.NetworkAccess, args.ConnectionProfiles));
        };
    }

    private static NetworkState GetNetworkState(NetworkAccess access, IEnumerable<ConnectionProfile> profiles)
    {
        if (access != NetworkAccess.None && access != NetworkAccess.Unknown)
        {
            return profiles.Any(x => x == ConnectionProfile.Ethernet || x == ConnectionProfile.WiFi)
                ? NfcSample.FormsApp.Components.Device.NetworkState.ConnectedHighSpeed
                : NfcSample.FormsApp.Components.Device.NetworkState.Connected;
        }

        return NfcSample.FormsApp.Components.Device.NetworkState.Disconnected;
    }

    public NetworkState GetNetworkState() => GetNetworkState(Connectivity.NetworkAccess, Connectivity.ConnectionProfiles);

    public abstract void SetOrientation(Orientation orientation);

    public abstract string? GetVersion();
}
