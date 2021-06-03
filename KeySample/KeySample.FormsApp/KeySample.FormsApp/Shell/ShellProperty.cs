namespace KeySample.FormsApp.Shell
{
    using Xamarin.Forms;

    public static class ShellProperty
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.CreateAttached(
            "Title",
            typeof(string),
            typeof(ShellProperty),
            null,
            propertyChanged: PropertyChanged);

        public static string GetTitle(BindableObject view)
        {
            return (string)view.GetValue(TitleProperty);
        }

        public static void SetTitle(BindableObject view, string value)
        {
            view.SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty FunctionVisibleProperty = BindableProperty.CreateAttached(
            "FunctionVisible",
            typeof(bool),
            typeof(ShellProperty),
            true,
            propertyChanged: PropertyChanged);

        public static bool GetFunctionVisible(BindableObject view)
        {
            return (bool)view.GetValue(FunctionVisibleProperty);
        }

        public static void SetFunctionVisible(BindableObject view, bool value)
        {
            view.SetValue(FunctionVisibleProperty, value);
        }

        public static readonly BindableProperty Function1TextProperty = BindableProperty.CreateAttached(
            "Function1Text",
            typeof(string),
            typeof(ShellProperty),
            string.Empty,
            propertyChanged: PropertyChanged);

        public static string GetFunction1Text(BindableObject view)
        {
            return (string)view.GetValue(Function1TextProperty);
        }

        public static void SetFunction1Text(BindableObject view, string value)
        {
            view.SetValue(Function1TextProperty, value);
        }

        public static readonly BindableProperty Function4TextProperty = BindableProperty.CreateAttached(
            "Function4Text",
            typeof(string),
            typeof(ShellProperty),
            string.Empty,
            propertyChanged: PropertyChanged);

        public static string GetFunction4Text(BindableObject view)
        {
            return (string)view.GetValue(Function4TextProperty);
        }

        public static void SetFunction4Text(BindableObject view, string value)
        {
            view.SetValue(Function4TextProperty, value);
        }

        public static readonly BindableProperty Function3TextProperty = BindableProperty.CreateAttached(
            "Function3Text",
            typeof(string),
            typeof(ShellProperty),
            string.Empty,
            propertyChanged: PropertyChanged);

        public static readonly BindableProperty Function1EnabledProperty = BindableProperty.CreateAttached(
            "Function1Enabled",
            typeof(bool),
            typeof(ShellProperty),
            false,
            propertyChanged: PropertyChanged);

        public static bool GetFunction1Enabled(BindableObject view)
        {
            return (bool)view.GetValue(Function1EnabledProperty);
        }

        public static void SetFunction1Enabled(BindableObject view, bool value)
        {
            view.SetValue(Function1EnabledProperty, value);
        }

        public static readonly BindableProperty Function4EnabledProperty = BindableProperty.CreateAttached(
            "Function4Enabled",
            typeof(bool),
            typeof(ShellProperty),
            false,
            propertyChanged: PropertyChanged);

        public static bool GetFunction4Enabled(BindableObject view)
        {
            return (bool)view.GetValue(Function4EnabledProperty);
        }

        public static void SetFunction4Enabled(BindableObject view, bool value)
        {
            view.SetValue(Function4EnabledProperty, value);
        }

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var parent = ((ContentView)bindable).Parent;
            if (parent?.BindingContext is IShellControl shell)
            {
                UpdateShellControl(shell, bindable);
            }
        }

        public static void UpdateShellControl(IShellControl shell, BindableObject? bindable)
        {
            if (bindable is null)
            {
                shell.Title.Value = string.Empty;
                shell.FunctionVisible.Value = false;
                shell.Function1Text.Value = string.Empty;
                shell.Function4Text.Value = string.Empty;
                shell.Function1Enabled.Value = false;
                shell.Function4Enabled.Value = false;
            }
            else
            {
                shell.Title.Value = GetTitle(bindable);
                shell.FunctionVisible.Value = GetFunctionVisible(bindable);
                shell.Function1Text.Value = GetFunction1Text(bindable);
                shell.Function4Text.Value = GetFunction4Text(bindable);
                shell.Function1Enabled.Value = GetFunction1Enabled(bindable);
                shell.Function4Enabled.Value = GetFunction4Enabled(bindable);
            }
        }
    }
}
