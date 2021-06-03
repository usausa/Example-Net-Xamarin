namespace KeySample.FormsApp.Modules.Control
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.Navigation;

    public class ControlMenuViewModel : AppViewModelBase
    {
        public ICommand ForwardCommand { get; }

        public ControlMenuViewModel(
            ApplicationState applicationState)
            : base(applicationState)
        {
            ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
