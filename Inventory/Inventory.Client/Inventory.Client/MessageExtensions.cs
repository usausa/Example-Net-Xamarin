namespace Inventory.Client
{
    using System.Text;
    using System.Threading.Tasks;

    using Inventory.Client.Components;

    using Smart.Forms.Components;

    public static class MessageExtensions
    {
        public static Task DisplayInformation(
            this IDialogService dialogService,
            string title,
            string message)
        {
            return dialogService.DisplayAlert(title, message, "OK");
        }

        public static Task<bool> DisplayConfirm(
            this IDialogService dialogService,
            string title,
            string message)
        {
            return dialogService.DisplayAlert(title, message, "OK", "Cancel");
        }

        public static Task DisplayNetworkError(
            this IDialogService dialogService,
            NetworkResult result)
        {
            var message = new StringBuilder();
            message.Append("Status = ").Append(result.Status).AppendLine();
            message.Append("Code = ").Append(result.StatusCode);

            return dialogService.DisplayAlert("通信エラー", message.ToString(), "OK");
        }
    }
}
