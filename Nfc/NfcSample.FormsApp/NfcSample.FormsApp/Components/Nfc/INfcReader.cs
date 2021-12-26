namespace NfcSample.FormsApp.Components.Nfc
{
    public interface INfcReader
    {
        public NfcType NfcType { get; set; }

        public bool Enable { get; set; }

        IObservable<INfc> TechDiscovered { get; }
    }
}
