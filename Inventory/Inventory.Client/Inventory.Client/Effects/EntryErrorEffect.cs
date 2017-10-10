namespace Inventory.Client.Effects
{
    using System.Linq;
    using Xamarin.Forms;

    public class EntryErrorEffect : RoutingEffect
    {
        public static readonly BindableProperty ApplyProperty =
            BindableProperty.Create("Apply", typeof(bool), typeof(MaxLengthEffect), false, propertyChanged: OnApplyPropertyChanged);

        public static readonly BindableProperty ErrorColorProperty =
            BindableProperty.Create("ErrorColor", typeof(Color), typeof(MaxLengthEffect), Color.Transparent);

        public static bool GetApply(BindableObject view)
        {
            return (bool)view.GetValue(ApplyProperty);
        }

        public static void SetApply(BindableObject view, bool value)
        {
            view.SetValue(ApplyProperty, value);
        }

        public static Color GetErrorColor(BindableObject view)
        {
            return (Color)view.GetValue(ErrorColorProperty);
        }

        public static void SetErrorColor(BindableObject view, Color value)
        {
            view.SetValue(ErrorColorProperty, value);
        }

        private static void OnApplyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is View view)
            {
                var effect = view.Effects.FirstOrDefault(x => x is EntryErrorEffect);
                if (effect != null)
                {
                    view.Effects.Remove(effect);
                }

                var apply = (bool)newValue;
                if (apply)
                {
                    view.Effects.Add(new EntryErrorEffect());
                }
            }
        }

        public EntryErrorEffect()
            : base("Inventory.EntryErrorEffect")
        {
        }
    }
}
