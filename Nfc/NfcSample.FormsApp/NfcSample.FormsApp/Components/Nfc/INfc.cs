namespace NfcSample.FormsApp.Components.Nfc
{
    public interface INfc
    {
        NfcType NfcType { get; }

        byte[] Id { get; }

        byte[] Access(byte[] command);
    }
}
