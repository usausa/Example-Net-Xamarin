namespace KeySample.FormsApp.Input
{
    using System;
    using System.Reflection;
    using System.Windows.Input;

    using Smart.Forms.Interactivity;

    using Xamarin.Forms;

    public sealed class ShortcutBehavior : BehaviorBase<Element>, IShortcutBehavior
    {
        public static readonly BindableProperty KeyCodeProperty = BindableProperty.Create(
            nameof(KeyCode),
            typeof(KeyCode),
            typeof(ShortcutBehavior));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(ShortcutBehavior));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(ShortcutBehavior));

        public static readonly BindableProperty ParameterPropertyProperty = BindableProperty.Create(
            nameof(ParameterProperty),
            typeof(string),
            typeof(ShortcutBehavior));

        public static readonly BindableProperty ConverterProperty = BindableProperty.Create(
            nameof(Converter),
            typeof(IValueConverter),
            typeof(ShortcutBehavior));

        public static readonly BindableProperty ConverterParameterProperty = BindableProperty.Create(
            nameof(ConverterParameter),
            typeof(object),
            typeof(ShortcutBehavior));

        public KeyCode KeyCode
        {
            get => (KeyCode)GetValue(KeyCodeProperty);
            set => SetValue(KeyCodeProperty, value);
        }

        public ICommand? Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public string? ParameterProperty
        {
            get => (string)GetValue(ParameterPropertyProperty);
            set => SetValue(ParameterPropertyProperty, value);
        }

        public IValueConverter? Converter
        {
            get => (IValueConverter)GetValue(ConverterProperty);
            set => SetValue(ConverterProperty, value);
        }

        public object? ConverterParameter
        {
            get => GetValue(ConverterParameterProperty);
            set => SetValue(ConverterParameterProperty, value);
        }

        public bool Handle(KeyCode key)
        {
            if ((KeyCode != key) || (AssociatedObject is null))
            {
                return false;
            }

            var command = Command;
            if (command is null)
            {
                return false;
            }

            var parameter = CommandParameter;

            if ((parameter is null) && !IsSet(CommandParameterProperty))
            {
                var property = ParameterProperty;
                if (!String.IsNullOrEmpty(property))
                {
                    object? value = AssociatedObject;
                    foreach (var part in property.Split('.'))
                    {
                        if (value is null)
                        {
                            break;
                        }

                        var pi = value.GetType().GetRuntimeProperty(part);
                        value = pi.GetValue(value);
                    }

                    parameter = Converter?.Convert(value, typeof(object), ConverterParameter, null) ?? value;
                }
            }

            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }

            return true;
        }
    }
}
