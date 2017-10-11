namespace Inventory.Client.Droid.Components
{
    using Android.App;

    using Inventory.Client.Components;

    using Xamarin.Forms;

    public class LoadingService : ILoadingService
    {
        private ProgressDialog progress;

        public bool Visible => progress != null;

        public void Show(string message)
        {
            progress?.Dismiss();

            progress = new ProgressDialog(Forms.Context);
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage(message);
            progress.Indeterminate = true;
            progress.Show();
        }

        public void Hide()
        {
            progress?.Dismiss();
            progress = null;
        }
    }
}