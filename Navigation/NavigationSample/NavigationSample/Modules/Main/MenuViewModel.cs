namespace NavigationSample.Modules.Main
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class MenuViewModel : AppViewModelBase
    {
        public static MenuViewModel DesignInstance { get; } = null; // For design

        public AsyncCommand<ViewId> ForwardCommand { get; }

        public AsyncCommand<ViewId> SharedCommand { get; }

        public MenuViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
            SharedCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(ViewId.SharedInput, Parameters.MakeNextViewId(x)));
        }
    }
}
