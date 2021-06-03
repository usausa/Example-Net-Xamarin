namespace KeySample.FormsApp.Components.Barcode
{
    using Smart;

    using Xamarin.Forms;

    public class AttachableEntryBarcodeReader : IAttachableBarcodeReader
    {
        private IAttachableBarcodeController? barcodeController;

        private Entry? target;

        public void Attach(IAttachableBarcodeController? controller)
        {
            if (barcodeController is not null)
            {
                barcodeController.Read -= BarcodeControllerOnRead;
            }

            barcodeController = controller;

            if (barcodeController is not null)
            {
                barcodeController.Read += BarcodeControllerOnRead;
            }
        }

        public void Listen(Entry? entry)
        {
            target = entry;
        }

        private void BarcodeControllerOnRead(object sender, EventArgs<string> e)
        {
            if (target is not null)
            {
                target.Text = e.Data;
                target.SendCompleted();
            }
        }
    }
}
