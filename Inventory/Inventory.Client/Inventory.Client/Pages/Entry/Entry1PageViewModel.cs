namespace Inventory.Client.Pages.Entry
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    using Inventory.Client.Models;
    using Inventory.Client.Models.Stack;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.Navigation.Plugins.Parameter;
    using Smart.Forms.ViewModels;
    using Smart.Reactive;

    public class Entry1PageViewModel : DisposableViewModelBase
    {
        private readonly INavigator navigator;

        private readonly NotificationValue<bool> validate = new NotificationValue<bool>();

        [Parameter(Direction.Export)]
        public int StorageNo { get; set; }

        public SimpleStack Stack { get; } = new SimpleStack(Length.StorageNo);

        public DelegateCommand<string> KeyPressCommand { get; }

        public AsyncCommand BackCommand { get; }

        public AsyncCommand NextCommand { get; }

        public Entry1PageViewModel(INavigator navigator)
        {
            this.navigator = navigator;

            BackCommand = MakeBusyCommand(Back);
            NextCommand = MakeBusyCommand(Next, () => validate.Value).Observe(validate);
            KeyPressCommand = new DelegateCommand<string>(KeyPress);

            Stack.PropertyChangedAsObservable(nameof(Stack.Value))
                .Select(stack => (stack.Value.Length > 0) && (Convert.ToInt32(stack.Value) > 0))
                .SubscribeValue(validate)
                .AddTo(Disposables);
        }

        private async Task Back()
        {
            await navigator.ForwardAsync("/MenuPage");
        }

        private async Task Next()
        {
            StorageNo = Convert.ToInt32(Stack.Value);
            await navigator.ForwardAsync("Entry2Page");
        }

        private void KeyPress(string key)
        {
            Stack.Push(key);
        }
    }
}
