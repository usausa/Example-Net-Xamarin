namespace KeySample.FormsApp.Behaviors
{
    using System.Linq;

    using KeySample.FormsApp.Components.Barcode;

    using Smart.Forms.Interactivity;

    using Xamarin.Forms;

    public sealed class BarcodeEntryBehavior : BehaviorBase<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject!.Focused += AssociatedObjectOnFocused;
            AssociatedObject!.Unfocused += AssociatedObjectOnUnfocused;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            AssociatedObject!.Focused -= AssociatedObjectOnFocused;
            AssociatedObject!.Unfocused -= AssociatedObjectOnUnfocused;

            base.OnDetachingFrom(bindable);
        }

        private void AssociatedObjectOnFocused(object sender, FocusEventArgs e)
        {
            if (AssociatedObject is not null)
            {
                BarcodeEntry.GetReader(AssociatedObject)?.Listen(AssociatedObject);
            }
        }

        private void AssociatedObjectOnUnfocused(object sender, FocusEventArgs e)
        {
            if (AssociatedObject is not null)
            {
                BarcodeEntry.GetReader(AssociatedObject)?.Listen(null);
            }
        }
    }

    public static class BarcodeEntry
    {
        public static readonly BindableProperty ReaderProperty = BindableProperty.CreateAttached(
            "Reader",
            typeof(IEntryBarcodeReader),
            typeof(BarcodeEntry),
            null,
            propertyChanged: BindChanged);

        public static IEntryBarcodeReader? GetReader(BindableObject view)
        {
            return (IEntryBarcodeReader?)view.GetValue(ReaderProperty);
        }

        public static void SetReader(BindableObject view, IEntryBarcodeReader? value)
        {
            view.SetValue(ReaderProperty, value);
        }

        private static void BindChanged(BindableObject bindable, object? oldValue, object? newValue)
        {
            if (bindable is not Entry entry)
            {
                return;
            }

            if (oldValue is not null)
            {
                var behavior = entry.Behaviors.FirstOrDefault(x => x is BarcodeEntryBehavior);
                if (behavior is not null)
                {
                    entry.Behaviors.Remove(behavior);
                }

                if (entry.IsFocused)
                {
                    ((IEntryBarcodeReader)oldValue).Listen(null);
                }
            }

            if (newValue is not null)
            {
                entry.Behaviors.Add(new BarcodeEntryBehavior());
            }
        }
    }
}
