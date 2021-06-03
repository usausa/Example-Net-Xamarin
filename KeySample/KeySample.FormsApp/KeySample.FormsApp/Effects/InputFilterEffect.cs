namespace KeySample.FormsApp.Effects
{
    using System;
    using System.Linq;

    using Xamarin.Forms;

    public sealed class InputFilterEffect : RoutingEffect
    {
        public static readonly BindableProperty RuleProperty = BindableProperty.CreateAttached(
            "Rule",
            typeof(Func<string, bool>),
            typeof(InputFilterEffect),
            null,
            propertyChanged: OnChanged);

        public static Func<string, bool>? GetRule(BindableObject view)
        {
            return (Func<string, bool>?)view.GetValue(RuleProperty);
        }

        public static void SetRule(BindableObject view, Func<string, bool?> value)
        {
            view.SetValue(RuleProperty, value);
        }

        private static void OnChanged(BindableObject bindable, object? oldValue, object? newValue)
        {
            if (bindable is not Element element)
            {
                return;
            }

            if (oldValue is not null)
            {
                var effect = element.Effects.FirstOrDefault(x => x is InputFilterEffect);
                if (effect != null)
                {
                    element.Effects.Remove(effect);
                }
            }

            if (newValue is not null)
            {
                element.Effects.Add(new InputFilterEffect());
            }
        }

        public InputFilterEffect()
            : base("KeySample.InputFilterEffect")
        {
        }
    }
}
