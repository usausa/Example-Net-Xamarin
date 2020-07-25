namespace NavigationSample.Modules.Stack
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack3ViewModel : AppViewModelBase
    {
        public static Stack3ViewModel DesignInstance { get; } = null; // For design

        public AsyncCommand<int> PopCommand { get; }

        public Stack3ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            PopCommand = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
        }

        protected override Task OnNotifyBackAsync()
        {
            return Navigator.PopAsync(1);
        }
    }
}
