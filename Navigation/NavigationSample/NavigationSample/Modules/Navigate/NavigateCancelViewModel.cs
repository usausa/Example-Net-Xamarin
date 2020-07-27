namespace NavigationSample.Modules.Navigate
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    using XamarinFormsComponents.Dialogs;

    public class NavigateCancelViewModel : AppViewModelBase
    {
        public static NavigateCancelViewModel DesignInstance { get; } = null; // For design

        private readonly IDialogs dialogs;

        public AsyncCommand BackCommand { get; }

        public NavigateCancelViewModel(
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
                await Navigator.PostActionAsync(() => ExecuteBusyAsync(async () =>
                {
                    if (await dialogs.Confirm("Cancel ?", acceptButton: "Yes", cancelButton: "No"))
                    {
                        await Navigator.ForwardAsync(ViewId.Menu);
                    }
                }));
            }
        }

        protected override Task OnNotifyBackAsync()
        {
            return Navigator.ForwardAsync(ViewId.Menu);
        }
    }
}
