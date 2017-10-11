using System;

[assembly: Xamarin.Forms.ExportEffect(typeof(Inventory.Client.Droid.Effects.EntryErrorEffect), "EntryErrorEffect")]

namespace Inventory.Client.Droid.Effects
{
    using System.ComponentModel;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class EntryErrorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            Update();
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == Client.Effects.EntryErrorEffect.ErrorColorProperty.PropertyName)
            {
                Update();
            }
        }

        private void Update()
        {
            if (Control == null)
            {
                return;
            }

            try
            {
                var color = Client.Effects.EntryErrorEffect.GetErrorColor(Element);
                if (color != Color.Transparent)
                {
                    Control.Background.SetColorFilter(color.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
                }
                else
                {
                    Control.Background.ClearColorFilter();
                    Control.RefreshDrawableState();
                }
            }
            catch (ObjectDisposedException)
            {
                // MEMO How should it be done?
            }
        }
    }
}