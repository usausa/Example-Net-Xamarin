namespace NavigationSample.Modules.Wizard
{
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    public class WizardInput1ViewModel : AppViewModelBase
    {
        [Scope]
        public NotificationValue<WizardContext> Context { get; } = new();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand NextCommand { get; }

        public WizardInput1ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            BackCommand = MakeAsyncCommand(OnNotifyBackAsync);
            NextCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.WizardInput2));
        }

        protected override Task OnNotifyBackAsync()
        {
            return Navigator.ForwardAsync(ViewId.Menu);
        }
    }
}
