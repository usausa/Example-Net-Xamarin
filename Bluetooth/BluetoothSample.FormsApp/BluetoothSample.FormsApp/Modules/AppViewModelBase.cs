namespace BluetoothSample.FormsApp.Modules
{
    using System.Threading.Tasks;

    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    using BluetoothSample.FormsApp.Shell;

    public class AppViewModelBase : ViewModelBase, INavigatorAware, INavigationEventSupport, INotifySupportAsync<ShellEvent>
    {
        public INavigator Navigator { get; set; } = default!;

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
                case ShellEvent.Function2:
                    return OnNotifyFunction2();
                case ShellEvent.Function3:
                    return OnNotifyFunction3();
                case ShellEvent.Function4:
                    return OnNotifyFunction4();
                default:
                    return Task.CompletedTask;
            }
        }

        protected virtual Task OnNotifyBackAsync() => Task.CompletedTask;

        protected virtual Task OnNotifyFunction1() => Task.CompletedTask;

        protected virtual Task OnNotifyFunction2() => Task.CompletedTask;

        protected virtual Task OnNotifyFunction3() => Task.CompletedTask;

        protected virtual Task OnNotifyFunction4() => Task.CompletedTask;
    }
}
