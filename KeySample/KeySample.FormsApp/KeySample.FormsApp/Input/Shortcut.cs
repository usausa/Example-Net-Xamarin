namespace KeySample.FormsApp.Input
{
    using Xamarin.Forms;

    public static class Shortcut
    {
        public static readonly BindableProperty KeyProperty = BindableProperty.CreateAttached(
            "Key",
            typeof(KeyCode),
            typeof(Shortcut),
            null);

        public static KeyCode GetKey(BindableObject bindable) => (KeyCode)bindable.GetValue(KeyProperty);

        public static void SetKey(BindableObject bindable, KeyCode value) => bindable.SetValue(KeyProperty, value);
    }
}
