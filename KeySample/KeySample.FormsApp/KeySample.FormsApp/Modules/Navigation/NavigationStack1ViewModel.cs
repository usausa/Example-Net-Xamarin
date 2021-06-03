namespace KeySample.FormsApp.Modules.Navigation
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.Navigation;

    public class NavigationStack1ViewModel : AppViewModelBase
    {
        public ICommand PushCommand { get; }

        public NavigationStack1ViewModel(
            ApplicationState applicationState)
            : base(applicationState)
        {
            PushCommand = MakeAsyncCommand(() => Navigator.PushAsync(ViewId.NavigationStack2));
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
