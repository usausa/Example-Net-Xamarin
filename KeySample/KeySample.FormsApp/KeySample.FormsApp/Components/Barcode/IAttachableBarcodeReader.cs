namespace KeySample.FormsApp.Components.Barcode
{
    public interface IAttachableBarcodeReader : IEntryBarcodeReader
    {
        void Attach(IAttachableBarcodeController? controller);
    }
}
