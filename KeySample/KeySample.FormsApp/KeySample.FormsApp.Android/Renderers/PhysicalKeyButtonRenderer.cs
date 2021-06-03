[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Button), typeof(KeySample.FormsApp.Droid.Renderers.PhysicalKeyButtonRenderer))]

namespace KeySample.FormsApp.Droid.Renderers
{
    using Android.Content;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class PhysicalKeyButtonRenderer : Xamarin.Forms.Platform.Android.FastRenderers.ButtonRenderer
    {
        public PhysicalKeyButtonRenderer(Context context)
            : base(context)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Element is not null)
                {
                    Element.FocusChangeRequested -= OnFocusChangeRequested;
                }
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement is not null)
            {
                e.OldElement.FocusChangeRequested -= OnFocusChangeRequested;
            }

            if (e.NewElement is not null)
            {
                Element.FocusChangeRequested += OnFocusChangeRequested;
            }
        }

        private void OnFocusChangeRequested(object sender, VisualElement.FocusRequestArgs e)
        {
            e.Result = Control?.RequestFocus() ?? false;
        }
    }
}
