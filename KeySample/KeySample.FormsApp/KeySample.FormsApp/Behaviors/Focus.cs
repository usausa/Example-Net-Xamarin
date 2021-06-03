namespace KeySample.FormsApp.Behaviors
{
    using Xamarin.Forms;

    public static class Focus
    {
        public static readonly BindableProperty DefaultProperty = BindableProperty.CreateAttached(
            "Default",
            typeof(bool),
            typeof(Focus),
            false);

        public static bool GetDefault(BindableObject view)
        {
            return (bool)view.GetValue(DefaultProperty);
        }

        public static void SetDefault(BindableObject view, bool value)
        {
            view.SetValue(DefaultProperty, value);
        }
    }
}
