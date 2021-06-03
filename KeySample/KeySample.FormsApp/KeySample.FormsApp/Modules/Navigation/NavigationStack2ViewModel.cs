namespace KeySample.FormsApp.Modules.Navigation
{
    using System.Threading.Tasks;

    using Smart.Navigation;

    public class NavigationStack2ViewModel : AppViewModelBase
    {
        public NavigationStack2ViewModel(
            ApplicationState applicationState)
            : base(applicationState)
        {
        }

        protected override Task OnNotifyBackAsync() => Navigator.PopAsync();

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
