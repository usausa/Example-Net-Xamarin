namespace KeySample.FormsApp.Components.Dialog
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using XamarinFormsComponents.Dialogs;

    public abstract class ApplicationDialogBase : IApplicationDialog
    {
        private readonly IDialogs dialogs;

        protected ApplicationDialogBase(IDialogs dialogs)
        {
            this.dialogs = dialogs;
        }

        public abstract ValueTask Information(string message, string? title = null, string ok = "OK");

        public abstract ValueTask<bool> Confirm(string message, bool defaultPositive = false, string? title = null, string ok = "OK", string cancel = "Cancel");

        public abstract ValueTask<int> Select(string[] items, int selected = -1, string? title = null);

        public IProgress Progress(string title) => dialogs.Progress(title);

        public IProgress Loading(string title) => dialogs.Loading(title);
    }

    public static class ApplicationDialogBaseExtensions
    {
        public static ValueTask<int> Select<T>(
            this IApplicationDialog dialog,
            T[] values,
            Func<T, string> formatter,
            int selected = -1,
            string? title = null)
        {
            var items = values.Select(formatter).ToArray();
            return dialog.Select(items, selected, title);
        }

        public static async ValueTask<T?> SelectItem<T>(
            this IApplicationDialog dialog,
            T[] values,
            Func<T, string> formatter,
            int selected = -1,
            string? title = null)
        {
            var items = values.Select(formatter).ToArray();
            var result = await dialog.Select(items, selected, title);
            return result >= 0 ? values[result] : default;
        }
    }
}
