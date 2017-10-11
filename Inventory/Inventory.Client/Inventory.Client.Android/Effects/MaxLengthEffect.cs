[assembly: Xamarin.Forms.ExportEffect(typeof(Inventory.Client.Droid.Effects.MaxLengthEffect), "MaxLengthEffect")]

namespace Inventory.Client.Droid.Effects
{
    using Android.Text;
    using Android.Widget;

    using Xamarin.Forms.Platform.Android;

    public class MaxLengthEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var length = Client.Effects.MaxLengthEffect.GetMaxLength(Element);

            var textView = Control as TextView;
            textView?.SetFilters(new IInputFilter[]
            {
                new InputFilterLengthFilter(length)
            });
        }

        protected override void OnDetached()
        {
        }
    }
}