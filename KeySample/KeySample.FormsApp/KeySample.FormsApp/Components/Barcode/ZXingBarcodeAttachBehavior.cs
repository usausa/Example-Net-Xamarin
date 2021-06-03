namespace KeySample.FormsApp.Components.Barcode
{
    using System;

    using Smart;
    using Smart.Forms.Interactivity;

    using Xamarin.Forms;

    using ZXing;
    using ZXing.Net.Mobile.Forms;

    public sealed class ZXingBarcodeAttachBehavior : BehaviorBase<ZXingScannerView>, IAttachableBarcodeController
    {
        public event EventHandler<EventArgs<string>>? Read;

        public static readonly BindableProperty ConnectProperty = BindableProperty.Create(
            nameof(Connect),
            typeof(IAttachableBarcodeReader),
            typeof(ZXingBarcodeAttachBehavior),
            null,
            propertyChanged: HandleConnectPropertyChanged);

        public IAttachableBarcodeReader? Connect
        {
            get => (IAttachableBarcodeReader)GetValue(ConnectProperty);
            set => SetValue(ConnectProperty, value);
        }

        protected override void OnAttachedTo(ZXingScannerView bindable)
        {
            base.OnAttachedTo(bindable);

            Connect?.Attach(this);
            AssociatedObject!.OnScanResult += OnScanResult;
        }

        protected override void OnDetachingFrom(ZXingScannerView bindable)
        {
            AssociatedObject!.OnScanResult -= OnScanResult;
            Connect?.Attach(null);

            base.OnDetachingFrom(bindable);
        }

        private void OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(() => Read?.Invoke(this, new EventArgs<string>(result.Text)));
        }

        private static void HandleConnectPropertyChanged(BindableObject bindable, object? oldValue, object? newValue)
        {
            ((ZXingBarcodeAttachBehavior)bindable).HandleConnectPropertyChanged(oldValue as IAttachableBarcodeReader, newValue as IAttachableBarcodeReader);
        }

        private void HandleConnectPropertyChanged(IAttachableBarcodeReader? oldValue, IAttachableBarcodeReader? newValue)
        {
            if (oldValue is not null)
            {
                Connect?.Attach(null);
            }

            if (newValue is not null)
            {
                Connect?.Attach(this);
            }
        }
    }
}
