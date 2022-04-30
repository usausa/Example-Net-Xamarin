namespace KeySample.FormsApp.Input
{
    using System.Collections;
    using System.Windows.Input;

    using Smart.Forms.Interactivity;

    using Xamarin.Forms;

    public sealed class ListViewShortcutBehavior : BehaviorBase<ListView>, IShortcutBehavior
    {
        public static readonly BindableProperty KeyCodeProperty = BindableProperty.Create(
            nameof(KeyCode),
            typeof(KeyCode),
            typeof(ListViewShortcutBehavior));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(ListViewShortcutBehavior));

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

            var service = DependencyService.Get<IInputService>();
            var selected = service.ResolveSelectedPosition(AssociatedObject);
            if ((selected >= 0) && (AssociatedObject.ItemsSource is IList list))
            {
                var parameter = list[selected];

                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }

            return true;
        }
    }
}
