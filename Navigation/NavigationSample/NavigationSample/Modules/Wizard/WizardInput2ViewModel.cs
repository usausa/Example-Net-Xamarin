namespace NavigationSample.Modules.Wizard
{
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    public class WizardInput2ViewModel : AppViewModelBase
    {
        [Scope]
        public NotificationValue<WizardContext> Context { get; } = new();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand NextCommand { get; }

        public WizardInput2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            BackCommand = MakeAsyncCommand(OnNotifyBackAsync);
            NextCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.WizardResult));
        }

        protected override Task OnNotifyBackAsync()
        {
            return Navigator.ForwardAsync(ViewId.WizardInput1);
        }
    }
}
