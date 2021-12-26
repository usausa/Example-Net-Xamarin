namespace NfcSample.FormsApp.Droid.Components.Nfc;

using System;

using Android.Nfc;
using Android.Nfc.Tech;

using NfcSample.FormsApp.Components.Nfc;

public sealed class AndroidNfcF : INfc
{
    public NfcType NfcType => NfcType.TypeF;

    private readonly NfcF nfc;

    public byte[] Id { get; }

    public AndroidNfcF(byte[] id, NfcF nfc)
    {
        Id = id;
        this.nfc = nfc;
    }

    public byte[] Access(byte[] command)
    {
        System.Diagnostics.Debug.WriteLine("Command: " + BitConverter.ToString(command));
        try
        {
            var response = nfc.Transceive(command);
            System.Diagnostics.Debug.WriteLine("Response: " + BitConverter.ToString(response));
            return response ?? Array.Empty<byte>();
        }
        catch (TagLostException)
        {
            System.Diagnostics.Debug.WriteLine("Response: ");
            return Array.Empty<byte>();
        }
    }
}
