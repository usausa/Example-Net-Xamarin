namespace NavigationSample.Modules.Wizard
{
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    public class WizardResultViewModel : AppViewModelBase
    {
        public static WizardResultViewModel DesignInstance => null; // For design

        [Scope]
        public NotificationValue<WizardContext> Context { get; } = new();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand NextCommand { get; }

        public WizardResultViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            BackCommand = MakeAsyncCommand(OnNotifyBackAsync);
            NextCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));
        }

        protected override Task OnNotifyBackAsync()
        {
            return Navigator.ForwardAsync(ViewId.WizardInput2);
        }
    }
}
