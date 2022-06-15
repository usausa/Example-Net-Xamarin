namespace BluetoothSample.FormsApp.Droid.Components.Bluetooth
{
    using System;
    using System.Threading.Tasks;

    using Android.Bluetooth;
    using Android.Content;
    using Android.Util;

    public sealed class BluetoothHelper
    {
        private readonly Context context;

        private readonly BluetoothAdapter adapter;

        public BluetoothHelper(Context context)
        {
            this.context = context;
            adapter = ((BluetoothManager)context.GetSystemService(Context.BluetoothService)!).Adapter!;
        }

        public void EnableAdapter()
        {
            if (!adapter.IsEnabled)
            {
                adapter.Enable();
            }
        }

        public async ValueTask<BluetoothDevice?> FindDeviceAsync(Func<BluetoothDevice, bool> predicate)
        {
            var tcs = new TaskCompletionSource<BluetoothDevice?>();

            var receiver = new FindReceiver(tcs, predicate);
            var filter = new IntentFilter();
            filter.AddAction(BluetoothDevice.ActionFound);
            filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);
            context.RegisterReceiver(receiver, filter);

            if (!adapter.StartDiscovery())
            {
                context.UnregisterReceiver(receiver);
                return null;
            }

            var device = await tcs.Task;

            adapter.CancelDiscovery();

            context.UnregisterReceiver(receiver);

            return device;
        }

        public class FindReceiver : BroadcastReceiver
        {
            private readonly TaskCompletionSource<BluetoothDevice?> tcs;

            private readonly Func<BluetoothDevice, bool> predicate;

            public FindReceiver(TaskCompletionSource<BluetoothDevice?> tcs, Func<BluetoothDevice, bool> predicate)
            {
                this.tcs = tcs;
                this.predicate = predicate;
            }

            public override void OnReceive(Context? context, Intent? intent)
            {
                switch (intent!.Action)
                {
                    case BluetoothDevice.ActionFound:
                        var device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice)!;
                        Log.Debug(nameof(BluetoothHelper), $"[BluetoothDevice.ActionFound] {device.Name}");
                        if (predicate(device))
                        {
                            tcs.TrySetResult(device);
                        }
                        break;

                    case BluetoothAdapter.ActionDiscoveryFinished:
                        Log.Debug(nameof(BluetoothHelper), "[BluetoothAdapter.ActionDiscoveryFinished]");
                        tcs.TrySetResult(null);
                        break;
                }
            }
        }

        public async ValueTask<bool> BondAsync(BluetoothDevice device, byte[] pin)
        {
            var tcs = new TaskCompletionSource<bool>();

            var receiver = new BondReceiver(tcs, pin);
            var filter = new IntentFilter();
            filter.AddAction(BluetoothDevice.ActionPairingRequest);
            filter.AddAction(BluetoothDevice.ActionBondStateChanged);
            //filter.Priority = (int)IntentFilterPriority.HighPriority;
            context.RegisterReceiver(receiver, filter);

            // Timeout
            //var cts = new CancellationTokenSource(10_000);
            //cts.Token.Register(() => tcs.TrySetResult(false));

            if (!device.CreateBond())
            {
                context.UnregisterReceiver(receiver);
                return false;
            }

            var result = await tcs.Task;

            //cts.Dispose();
            context.UnregisterReceiver(receiver);

            return result;
        }

        public class BondReceiver : BroadcastReceiver
        {
            private readonly TaskCompletionSource<bool> tcs;

            private readonly byte[] pin;

            public BondReceiver(TaskCompletionSource<bool> tcs, byte[] pin)
            {
                this.tcs = tcs;
                this.pin = pin;
            }

            public override void OnReceive(Context? context, Intent? intent)
            {
                switch (intent!.Action)
                {
                    case BluetoothDevice.ActionPairingRequest:
                        var device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice)!;
                        Log.Debug(nameof(BluetoothHelper), $"[BluetoothDevice.ActionPairingRequest] {device.Name}");
                        if (pin.Length > 0)
                        {
                            device.SetPin(pin);
                        }
                        break;

                    case BluetoothDevice.ActionBondStateChanged:
                        var state = (Bond)intent.GetIntExtra(BluetoothDevice.ExtraBondState, BluetoothDevice.Error);
                        var previousState = (Bond)intent.GetIntExtra(BluetoothDevice.ExtraPreviousBondState, BluetoothDevice.Error);
                        Log.Debug(nameof(BluetoothHelper), $"[BluetoothDevice.ActionBondStateChanged] {previousState} -> {state}");
                        if (state == Bond.Bonded)
                        {
                            tcs.TrySetResult(true);
                        }
                        else if (state == Bond.None)
                        {
                            tcs.TrySetResult(false);
                        }
                        break;
                }
            }
        }
    }
}
