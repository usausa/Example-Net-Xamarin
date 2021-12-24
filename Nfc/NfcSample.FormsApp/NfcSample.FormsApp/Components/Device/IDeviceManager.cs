namespace NfcSample.FormsApp.Components.Device;

using System;

public interface IDeviceManager
{
    IObservable<NetworkState> NetworkState { get; }

    NetworkState GetNetworkState();

    void SetOrientation(Orientation orientation);

    string? GetVersion();
}
