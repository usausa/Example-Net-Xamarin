namespace NavigationSample.Modules.Navigate
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    using XamarinFormsComponents.Dialogs;

    public class NavigateInitializeViewModel : AppViewModelBase
    {
        public static NavigateInitializeViewModel DesignInstance { get; } = null; // For design

        private readonly IDialogs dialogs;

        public AsyncCommand BackCommand { get; }

        public NavigateInitializeViewModel(
            ApplicationState applicationState,
            IDialogs dialogs)
            : base(applicationState)
        {
            this.dialogs = dialogs;

            BackCommand = MakeAsyncCommand(OnNotifyBackAsync);
        }

        public override async void OnNavigatedTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                await Navigator.PostActionAsync(() => ExecuteBusyAsync(InitializeAsync));
            }
        }

        protected override Task OnNotifyBackAsync()
        {
            return Navigator.ForwardAsync(ViewId.Menu);
        }

        protected async Task InitializeAsync()
        {
            using (dialogs.Loading())
            {
                await Task.Delay(5000);
            }
        }
    }
}
