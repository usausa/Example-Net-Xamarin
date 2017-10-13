namespace Inventory.Client.Pages
{
    using System;
    using System.Threading.Tasks;

    using Inventory.Client.Components;
    using Inventory.Client.Services;

    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.ViewModels;

    public class MenuPageViewModel : ViewModelBase
    {
        private readonly INavigator navigator;

        private readonly Session session;

        public AsyncCommand<string> NavigateCommand { get; }

        public AsyncCommand<string> NavigateIfSettedCommand { get; }

        public AsyncCommand<string> NavigateIfItemExistCommand { get; }

        public AsyncCommand LogoutCommand { get; }

        public MenuPageViewModel(
            INavigator navigator,
            ISettingService settingService,
            ItemService itemService,
            Session session)
        {
            this.navigator = navigator;
            this.session = session;

            var setted = !String.IsNullOrEmpty(settingService.GetEndPoint());
            var itemExist = itemService.IsItemExists();

            NavigateCommand = MakeAsyncCommand<string>(Navigate);
            NavigateIfSettedCommand = MakeAsyncCommand<string>(Navigate, x => setted);
            NavigateIfItemExistCommand = MakeAsyncCommand<string>(Navigate, x => itemExist);
            LogoutCommand = MakeAsyncCommand(Logout);
        }

        private async Task Navigate(string page)
        {
            await navigator.ForwardAsync(page);
        }

        private async Task Logout()
        {
            session.UserId = 0;

            await navigator.ForwardAsync("/LoginPage");
        }
    }
}
