namespace NavigationSample.Modules.Shared
{
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class SharedInputViewModel : AppViewModelBase
    {
        public static SharedInputViewModel DesignInstance { get; } = null; // For design

        private ViewId nextViewId;

        public NotificationValue<string> No { get; } = new NotificationValue<string>();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand NextCommand { get; }

        public SharedInputViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            BackCommand = MakeAsyncCommand(OnNotifyBackAsync);
            NextCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(nextViewId, Parameters.MakeNextViewId(nextViewId).WithNo(No.Value)));
        }

        public override void OnNavigatedTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                nextViewId = context.Parameter.GetNextViewId();
            }
        }

        protected override Task OnNotifyBackAsync()
        {
            return Navigator.ForwardAsync(ViewId.Menu);
        }
    }
}
