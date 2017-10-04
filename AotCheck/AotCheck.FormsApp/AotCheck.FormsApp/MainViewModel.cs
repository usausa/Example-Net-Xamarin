using System.Windows.Input;
using Smart.Forms.Input;

namespace AotCheck.FormsApp
{
    using Smart.ComponentModel;

    public class MainViewModel
    {
        public NotificationValue<string> Runtime { get; } = new NotificationValue<string>("-");

        public NotificationValue<string> ExpressionActivator { get; } = new NotificationValue<string>("-");

        public NotificationValue<string> ExpressionGetter { get; } = new NotificationValue<string>("-");

        public NotificationValue<string> ExpressionSetter { get; } = new NotificationValue<string>("-");

        public NotificationValue<string> ReflectionActivator { get; } = new NotificationValue<string>("-");

        public NotificationValue<string> ReflectionGetter { get; } = new NotificationValue<string>("-");

        public NotificationValue<string> ReflectionSetter { get; } = new NotificationValue<string>("-");

        public ICommand ExecuteCommand { get; }

        public MainViewModel()
        {
            ExecuteCommand = new DelegateCommand(Execute);
        }

        private void Execute()
        {
            Runtime.Value = "ok";
        }
    }
}
