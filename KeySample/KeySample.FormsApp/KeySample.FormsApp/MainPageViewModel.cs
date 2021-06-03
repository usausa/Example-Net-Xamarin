namespace KeySample.FormsApp
{
    using System.Windows.Input;

    using KeySample.FormsApp.Shell;

    using Smart.ComponentModel;
    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class MainPageViewModel : ViewModelBase, IShellControl
    {
        public ApplicationState ApplicationState { get; }

        public INavigator Navigator { get; }

        public NotificationValue<string> Title { get; } = new();

        public NotificationValue<bool> FunctionVisible { get; } = new();

        public NotificationValue<string> Function1Text { get; } = new();
        public NotificationValue<string> Function4Text { get; } = new();

        public NotificationValue<bool> Function1Enabled { get; } = new();
        public NotificationValue<bool> Function4Enabled { get; } = new();

        public ICommand Function1Command { get; }
        public ICommand Function4Command { get; }

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public MainPageViewModel(
            ApplicationState applicationState,
            INavigator navigator)
            : base(applicationState)
        {
            ApplicationState = applicationState;
            Navigator = navigator;

            Function1Command = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(ShellEvent.Function1),
                    () => Function1Enabled.Value)
                .Observe(Function1Enabled);
            Function4Command = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(ShellEvent.Function4),
                    () => Function4Enabled.Value)
                .Observe(Function4Enabled);
        }
    }
}
