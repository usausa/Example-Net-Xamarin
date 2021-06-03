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

        public static KeyCode GetKey(BindableObject view)
        {
            return (KeyCode)view.GetValue(KeyProperty);
        }

        public static void SetKey(BindableObject view, KeyCode value)
        {
            view.SetValue(KeyProperty, value);
        }
    }
}
