namespace KeySample.FormsApp.Droid.Components.Dialog
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;
    using Android.Graphics.Drawables;
    using Android.Views;

    using KeySample.FormsApp.Components.Dialog;

    using XamarinFormsComponents.Dialogs;

    public class ApplicationDialog : ApplicationDialogBase
    {
        private readonly Activity activity;

        public ApplicationDialog(Activity activity, IDialogs dialogs)
            : base(dialogs)
        {
            this.activity = activity;
        }

        public async override ValueTask Information(string message, string? title = null, string ok = "OK")
        {
            var dialog = new InformationDialog(activity);
            await dialog.ShowAsync(message, title, ok);
        }

        public async override ValueTask<bool> Confirm(string message, bool defaultPositive = false, string? title = null, string ok = "OK", string cancel = "Cancel")
        {
            var dialog = new ConfirmDialog(activity);
            return await dialog.ShowAsync(message, defaultPositive, title, ok, cancel);
        }

        public async override ValueTask<int> Select(string[] items, int selected = -1, string? title = null)
        {
            var dialog = new SelectDialog(activity);
            return await dialog.ShowAsync(items, selected, title);
        }

        public class InformationDialog : Java.Lang.Object, IDialogInterfaceOnShowListener, IDialogInterfaceOnKeyListener
        {
            private readonly TaskCompletionSource<bool> result = new();

            private readonly Activity activity;

            [AllowNull]
            private AlertDialog alertDialog;

            public InformationDialog(Activity activity)
            {
                this.activity = activity;
            }

            public Task ShowAsync(string message, string? title, string ok)
            {
                alertDialog = new AlertDialog.Builder(activity)
                    .SetTitle(title)!
                    .SetMessage(message)!
                    .SetOnKeyListener(this)!
                    .SetCancelable(false)!
                    .Create()!;
                alertDialog.SetOnShowListener(this);
                alertDialog.SetButton((int)DialogButtonType.Positive, ok, (_, _) => result.TrySetResult(true));

                alertDialog.Show();

                return result.Task;
            }

            public void OnShow(IDialogInterface? dialog)
            {
                var button = alertDialog.GetButton((int)DialogButtonType.Positive)!;
                button.RequestFocus();
            }

            public bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? e)
            {
                if ((e!.KeyCode == Keycode.Del) && (e.Action == KeyEventActions.Up))
                {
                    dialog!.Dismiss();
                    result.TrySetResult(false);
                    return true;
                }

                return false;
            }
        }

        public class ConfirmDialog : Java.Lang.Object, IDialogInterfaceOnShowListener, IDialogInterfaceOnKeyListener
        {
            private readonly TaskCompletionSource<bool> result = new();

            private readonly Activity activity;

            [AllowNull]
            private AlertDialog alertDialog;

            private bool positive;

            public ConfirmDialog(Activity activity)
            {
                this.activity = activity;
            }

            public Task<bool> ShowAsync(string message, bool defaultPositive, string? title, string ok, string cancel)
            {
                positive = defaultPositive;

                alertDialog = new AlertDialog.Builder(activity)
                    .SetTitle(title)!
                    .SetMessage(message)!
                    .SetOnKeyListener(this)!
                    .SetCancelable(false)!
                    .Create()!;
                alertDialog.SetOnShowListener(this);
                alertDialog.SetButton((int)DialogButtonType.Positive, ok, (_, _) => result.TrySetResult(true));
                alertDialog.SetButton((int)DialogButtonType.Negative, cancel, (_, _) => result.TrySetResult(false));

                alertDialog.Show();

                return result.Task;
            }

            public void OnShow(IDialogInterface? dialog)
            {
                var button = alertDialog.GetButton(positive ? (int)DialogButtonType.Positive : (int)DialogButtonType.Negative)!;
                button.RequestFocus();
            }

            public bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? e)
            {
                if ((e!.KeyCode == Keycode.Del) && (e.Action == KeyEventActions.Up))
                {
                    dialog!.Dismiss();
                    result.TrySetResult(false);
                    return true;
                }

                return false;
            }
        }

        public class SelectDialog : Java.Lang.Object, IDialogInterfaceOnShowListener, IDialogInterfaceOnKeyListener
        {
            private readonly TaskCompletionSource<int> result = new();

            private readonly Activity activity;

            [AllowNull]
            private AlertDialog alertDialog;

            public SelectDialog(Activity activity)
            {
                this.activity = activity;
            }

            public Task<int> ShowAsync(string[] items, int selected, string? title)
            {
                alertDialog = new AlertDialog.Builder(activity)
                    .SetTitle(title)!
                    .SetItems(items, (_, args) => result.TrySetResult(args.Which))!
                    .SetOnKeyListener(this)!
                    .SetCancelable(false)!
                    .Create()!;
                alertDialog.SetOnShowListener(this);
                alertDialog.ListView!.Selector = new ColorDrawable(Android.Graphics.Color.Blue) { Alpha = 64 };

                alertDialog.Show();

                if (selected >= 0)
                {
                    alertDialog.ListView.SetSelection(selected);
                }

                return result.Task;
            }

            public void OnShow(IDialogInterface? dialog)
            {
                alertDialog.ListView?.RequestFocus();
            }

            public bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? e)
            {
                if ((e!.KeyCode == Keycode.Del) && (e.Action == KeyEventActions.Up))
                {
                    dialog!.Dismiss();
                    result.TrySetResult(-1);
                    return true;
                }

                return false;
            }
        }
    }
}
