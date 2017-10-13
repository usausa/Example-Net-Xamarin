namespace Inventory.Client.Pages.Edit
{
    using System.Threading.Tasks;

    using Inventory.Client.Models;
    using Inventory.Client.Models.Stack;

    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.ViewModels;

    public class QtyEditPageViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigator navigator;

        public NumberStack Stack { get; } = new NumberStack(Length.Qty);

        public AsyncCommand CancelCommand { get; }

        public AsyncCommand ApplyCommand { get; }

        public DelegateCommand<string> KeyPressCommand { get; }

        public QtyEditPageViewModel(INavigator navigator)
        {
            this.navigator = navigator;

            CancelCommand = MakeAsyncCommand(Cancel);
            ApplyCommand = MakeAsyncCommand(Apply);
            KeyPressCommand = new DelegateCommand<string>(KeyPress);
        }

        public void OnNavigatingTo(NavigationContext context)
        {
            if (!context.IsPopBack)
            {
                Stack.ResetValue = context.Parameters.GetValue<long>(EditParameter.ResetValue);
                Stack.Value = context.Parameters.GetValue<long>(EditParameter.Value);
            }
        }

        public void OnNavigatedTo(NavigationContext context)
        {
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
        }

        private async Task Cancel()
        {
            await navigator.PopModalAsync();
        }

        private async Task Apply()
        {
            var parameters = new NavigationParameters()
                .SetValue(EditParameter.Value, Stack.Value);
            await navigator.PopModalAsync(parameters);
        }

        private void KeyPress(string key)
        {
            Stack.Push(key);
        }
    }
}
