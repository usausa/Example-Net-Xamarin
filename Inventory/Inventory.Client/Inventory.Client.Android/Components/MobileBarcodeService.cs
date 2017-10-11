namespace Inventory.Client.Droid.Components
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Inventory.Client.Components;

    using ZXing;
    using ZXing.Mobile;

    public class MobileBarcodeService : IBarcodeService
    {
        private readonly MobileBarcodeScanningOptions options = new MobileBarcodeScanningOptions
        {
            PossibleFormats = new List<BarcodeFormat>
            {
                BarcodeFormat.EAN_13,
                BarcodeFormat.EAN_8
            },
            TryHarder = true,
            TryInverted = true,
        };

        private bool scanning;

        public async Task<string> ScanAsync()
        {
            var scanner = new MobileBarcodeScanner();

            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 0, 1, 500), () =>
            {
                if (scanning)
                {
                    scanner.AutoFocus();
                    return true;
                }

                return false;
            });

            scanning = true;
            var result = await scanner.Scan(options);
            scanning = false;

            return result != null ? result.Text : string.Empty;
        }
    }
}