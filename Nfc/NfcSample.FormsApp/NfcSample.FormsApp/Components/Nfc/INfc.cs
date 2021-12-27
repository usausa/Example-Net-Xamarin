namespace NfcSample.FormsApp.Components.Nfc
{
    public interface INfc
    {
        byte[] Id { get; }

        byte[] Access(byte[] command);
    }
}
