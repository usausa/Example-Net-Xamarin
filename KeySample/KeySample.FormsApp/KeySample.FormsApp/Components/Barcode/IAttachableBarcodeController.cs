namespace KeySample.FormsApp.Components.Barcode
{
    using System;

    using Smart;

    public interface IAttachableBarcodeController
    {
        public event EventHandler<EventArgs<string>> Read;
    }
}
