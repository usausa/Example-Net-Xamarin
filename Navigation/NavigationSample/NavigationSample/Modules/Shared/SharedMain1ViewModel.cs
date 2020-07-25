namespace NavigationSample.Modules.Shared
{
    public class SharedMain1ViewModel : AppViewModelBase
    {
        public static SharedMain1ViewModel DesignInstance { get; } = null; // For design

        public SharedMain1ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }
    }
}
