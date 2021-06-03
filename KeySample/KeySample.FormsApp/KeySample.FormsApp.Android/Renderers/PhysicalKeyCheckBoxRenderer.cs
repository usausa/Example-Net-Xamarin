[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.CheckBox), typeof(KeySample.FormsApp.Droid.Renderers.PhysicalKeyCheckBoxRenderer))]

namespace KeySample.FormsApp.Droid.Renderers
{
    using Android.Content;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class PhysicalKeyCheckBoxRenderer : Xamarin.Forms.Platform.Android.CheckBoxRenderer
    {
        public PhysicalKeyCheckBoxRenderer(Context context)
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

        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
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
