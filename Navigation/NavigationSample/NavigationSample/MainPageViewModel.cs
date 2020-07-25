namespace NavigationSample
{
    using NavigationSample.Shell;

    using Smart.ComponentModel;
    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class MainPageViewModel : ViewModelBase, IShellControl
    {
        public static MainPageViewModel DesignInstance { get; } = null; // For design

        public NotificationValue<string> Title { get; } = new NotificationValue<string>();

        public ApplicationState ApplicationState { get; }

        public INavigator Navigator { get; }

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public MainPageViewModel(
            ApplicationState applicationState,
            INavigator navigator)
            : base(applicationState)
        {
            ApplicationState = applicationState;
            Navigator = navigator;
        }
    }
}
