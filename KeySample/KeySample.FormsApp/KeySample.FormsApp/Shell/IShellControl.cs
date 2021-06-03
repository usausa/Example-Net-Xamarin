namespace KeySample.FormsApp.Shell
{
    using Smart.ComponentModel;

    public interface IShellControl
    {
        NotificationValue<string> Title { get; }

        NotificationValue<bool> FunctionVisible { get; }

        NotificationValue<string> Function1Text { get; }

        NotificationValue<string> Function4Text { get; }

        NotificationValue<bool> Function1Enabled { get; }

        NotificationValue<bool> Function4Enabled { get; }
    }
}
