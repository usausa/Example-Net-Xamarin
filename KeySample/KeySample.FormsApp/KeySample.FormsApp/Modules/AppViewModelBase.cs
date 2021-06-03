namespace KeySample.FormsApp.Modules
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using KeySample.FormsApp.Shell;

    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class AppViewModelBase : ViewModelBase, INavigatorAware, INavigationEventSupport, INotifySupportAsync<ShellEvent>
    {
        [AllowNull]
        public INavigator Navigator { get; set; }

        public ApplicationState ApplicationState { get; }

        protected AppViewModelBase(ApplicationState applicationState)
            : base(applicationState)
        {
            ApplicationState = applicationState;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
        }

        public virtual void OnNavigatingFrom(INavigationContext context)
        {
        }

        public virtual void OnNavigatingTo(INavigationContext context)
        {
        }

        public virtual void OnNavigatedTo(INavigationContext context)
        {
        }

        public Task NavigatorNotifyAsync(ShellEvent parameter)
        {
            switch (parameter)
            {
                case ShellEvent.Back:
                    return OnNotifyBackAsync();
                case ShellEvent.Function1:
                    return OnNotifyFunction1();
                case ShellEvent.Function4:
                    return OnNotifyFunction4();
                default:
                    return Task.CompletedTask;
            }
        }

        protected virtual Task OnNotifyBackAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnNotifyFunction1()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnNotifyFunction4()
        {
            return Task.CompletedTask;
        }
    }
}
