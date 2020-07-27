namespace NavigationSample.Modules.Shared
{
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class SharedMain1ViewModel : AppViewModelBase
    {
        public static SharedMain1ViewModel DesignInstance { get; } = null; // For design

        public NotificationValue<string> No { get; } = new NotificationValue<string>();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand CompleteCommand { get; }

        public SharedMain1ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            BackCommand = MakeAsyncCommand(OnNotifyBackAsync);
            CompleteCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));
        }

        public override void OnNavigatedTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                No.Value = context.Parameter.GetNo();
            }
        }

        protected override Task OnNotifyBackAsync()
        {
            return Navigator.ForwardAsync(ViewId.SharedInput, Parameters.MakeNextViewId(ViewId.SharedMain1));
        }
    }
}
