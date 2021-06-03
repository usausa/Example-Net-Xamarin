namespace KeySample.FormsApp.Components.Barcode
{
    using Xamarin.Forms;

    public interface IEntryBarcodeReader
    {
        public void Listen(Entry? entry);
    }
}
