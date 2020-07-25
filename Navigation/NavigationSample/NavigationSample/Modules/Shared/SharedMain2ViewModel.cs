namespace NavigationSample.Modules.Shared
{
    public class SharedMain2ViewModel : AppViewModelBase
    {
        public static SharedMain2ViewModel DesignInstance { get; } = null; // For design

        public SharedMain2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }
    }
}
