namespace BluetoothSample.FormsApp.Markup
{
    using System;

    using BluetoothSample.FormsApp.Modules;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [ContentProperty("Value")]
    public sealed class ViewIdExtension : IMarkupExtension<ViewId>
    {
        public ViewId Value { get; set; }

        public ViewId ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}
