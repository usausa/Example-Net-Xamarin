namespace Inventory.Client.Pages
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    using Inventory.Client.Models;
    using Inventory.Client.Models.Stack;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.ViewModels;
    using Smart.Reactive;

    public class LoginPageViewModel : DisposableViewModelBase
    {
        private readonly INavigator navigator;

        private readonly Session session;

        private readonly NotificationValue<bool> validate = new NotificationValue<bool>();

        public SimpleStack Stack { get; } = new SimpleStack(Length.UserId);

        public AsyncCommand LoginCommand { get; }

        public DelegateCommand<string> KeyPressCommand { get; }

        public LoginPageViewModel(
            INavigator navigator,
            Session session)
        {
            this.navigator = navigator;
            this.session = session;

            LoginCommand = MakeBusyCommand(Login, () => validate.Value).Observe(validate);
            KeyPressCommand = new DelegateCommand<string>(KeyPress);

            Stack.PropertyChangedAsObservable(nameof(Stack.Value))
                .Select(stack => stack.Value.Length > 0)
                .SubscribeValue(validate)
                .AddTo(Disposables);
        }

        private async Task Login()
        {
            session.UserId = Convert.ToInt32(Stack.Value);

            await navigator.ForwardAsync("/MenuPage");
        }

        private void KeyPress(string key)
        {
            Stack.Push(key);
        }
    }
}
