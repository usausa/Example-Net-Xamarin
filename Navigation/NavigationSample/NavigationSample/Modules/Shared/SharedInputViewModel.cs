namespace NavigationSample.Modules.Shared
{
    public class SharedInputViewModel : AppViewModelBase
    {
        public static SharedInputViewModel DesignInstance { get; } = null; // For design

        public SharedInputViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }
    }
}
