namespace NfcSample.FormsApp.Modules;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Smart.Forms.ViewModels;
using Smart.Navigation;

using NfcSample.FormsApp.Shell;

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
        return parameter switch
        {
            ShellEvent.Back => OnNotifyBackAsync(),
            ShellEvent.Function1 => OnNotifyFunction1(),
            ShellEvent.Function2 => OnNotifyFunction2(),
            ShellEvent.Function3 => OnNotifyFunction3(),
            ShellEvent.Function4 => OnNotifyFunction4(),
            _ => Task.CompletedTask
        };
    }

    protected virtual Task OnNotifyBackAsync() => Task.CompletedTask;

    protected virtual Task OnNotifyFunction1() => Task.CompletedTask;

    protected virtual Task OnNotifyFunction2() => Task.CompletedTask;

    protected virtual Task OnNotifyFunction3() => Task.CompletedTask;

    protected virtual Task OnNotifyFunction4() => Task.CompletedTask;
}
