namespace NavigationSample.Shell
{
    using Smart.ComponentModel;

    public interface IShellControl
    {
        NotificationValue<string> Title { get; }
    }
}
