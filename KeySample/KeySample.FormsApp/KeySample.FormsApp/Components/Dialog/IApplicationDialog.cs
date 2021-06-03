namespace KeySample.FormsApp.Components.Dialog
{
    using System.Threading.Tasks;

    using XamarinFormsComponents.Dialogs;

    public interface IApplicationDialog
    {
        ValueTask Information(string message, string? title = null, string ok = "OK");

        ValueTask<bool> Confirm(string message, bool defaultPositive = false, string? title = null, string ok = "OK", string cancel = "Cancel");

        ValueTask<int> Select(string[] items, int selected = -1, string? title = null);

        IProgress Progress(string title);

        IProgress Loading(string title);
    }
}
