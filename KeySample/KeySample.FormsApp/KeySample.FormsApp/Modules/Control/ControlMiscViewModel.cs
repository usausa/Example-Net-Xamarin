namespace KeySample.FormsApp.Modules.Control
{
    using System.Threading.Tasks;

    using Smart.Navigation;

    public class ControlMiscViewModel : AppViewModelBase
    {
        public ControlMiscViewModel(
            ApplicationState applicationState)
            : base(applicationState)
        {
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.ControlMenu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
