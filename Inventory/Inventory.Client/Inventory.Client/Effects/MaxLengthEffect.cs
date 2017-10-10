namespace Inventory.Client.Effects
{
    using System.Linq;

    using Xamarin.Forms;

    public class MaxLengthEffect : RoutingEffect
    {
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthEffect), 0, propertyChanged: OnMaxLengthChanged);

        private static void OnMaxLengthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is View view)
            {
                var effect = view.Effects.FirstOrDefault(x => x is MaxLengthEffect);
                if (effect != null)
                {
                    view.Effects.Remove(effect);
                }

                var maxLength = (int)newValue;
                if (maxLength > 0)
                {
                    view.Effects.Add(new MaxLengthEffect());
                }
            }
        }

        public static int GetMaxLength(BindableObject view)
        {
            return (int)view.GetValue(MaxLengthProperty);
        }

        public static void SetMaxLength(BindableObject view, int value)
        {
            view.SetValue(MaxLengthProperty, value);
        }

        public MaxLengthEffect()
            : base("Inventory.MaxLengthEffect")
        {
        }
    }
}
