namespace BluetoothSample.FormsApp.Droid.Components.Printer
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Android.Bluetooth;
    using Android.Util;

    using BluetoothSample.FormsApp.Droid.Components.Bluetooth;

    using Java.Util;

    using IPrinter = BluetoothSample.FormsApp.Components.Printer.IPrinter;

    public class DummyPrinter : IPrinter
    {
        private static readonly UUID SppUuid = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB")!;

        private static readonly Func<BluetoothDevice, bool> Finder = x => (x.Name is not null) && x.Name.Contains("DummyPrinter");

        private static readonly byte[] Pin = Encoding.ASCII.GetBytes("8888");

        private readonly BluetoothHelper bluetooth;

        private BluetoothDevice? device;

        public DummyPrinter(BluetoothHelper bluetooth)
        {
            this.bluetooth = bluetooth;
        }

        public async ValueTask<bool> WriteAsync(string command)
        {
            bluetooth.EnableAdapter();

            // Find
            if (device is null)
            {
                device = await bluetooth.FindDeviceAsync(Finder);
                if (device is null)
                {
                    return false;
                }
            }

            // Bond
            if ((device.BondState != Bond.Bonded) && !await bluetooth.BondAsync(device, Pin))
            {
                device = null;
                return false;
            }

            // Execute
            var socket = default(BluetoothSocket);
            try
            {
                // TODO Timeout?
                socket = device.CreateRfcommSocketToServiceRecord(SppUuid)!;

                await socket.ConnectAsync();

                // Write
                var send = Encoding.ASCII.GetBytes(command);
                await socket.OutputStream!.WriteAsync(send, 0, send.Length);

                // Read
                var receive = new byte[16];
                var read = await socket.InputStream!.ReadAsync(receive, 0, receive.Length);
                if (read <= 0)
                {
                    device = null;
                    return false;
                }

                var result = Encoding.ASCII.GetString(receive, 0, read);
                return result.StartsWith("OK");
            }
            catch (Java.IO.IOException ex)
            {
                Log.Error(nameof(DummyPrinter), ex, "Connection error.");
                device = null;
                return false;
            }
            finally
            {
                socket?.Close();
            }
        }
    }
}
