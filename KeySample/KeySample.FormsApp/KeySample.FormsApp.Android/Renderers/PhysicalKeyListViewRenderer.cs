[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(KeySample.FormsApp.Droid.Renderers.PhysicalKeyListViewRenderer))]

namespace KeySample.FormsApp.Droid.Renderers
{
    using Android.Content;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Views;

    using Xamarin.Forms.Platform.Android;

    public class PhysicalKeyListViewRenderer : Xamarin.Forms.Platform.Android.ListViewRenderer
    {
        public PhysicalKeyListViewRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            // Focusable
            Control.SetFocusable(ViewFocusability.Focusable);

            // Selector
            //Control.SetDrawSelectorOnTop(true);
            var states = new StateListDrawable();
            states.AddState(new[] { Android.Resource.Attribute.StateFocused }, new PaintDrawable(Color.Blue) { Alpha = 64 });
            Control.Selector = states;
        }
    }
}
