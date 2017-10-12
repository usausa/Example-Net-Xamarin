namespace Inventory.Client.Pages.Sync
{
    using System.Threading.Tasks;

    using Inventory.Client.Components;
    using Inventory.Client.Services;

    using Smart.Forms.Components;
    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.ViewModels;

    public class MasterDownloadPageViewModel : DisposableViewModelBase
    {
        private readonly INavigator navigator;

        private readonly IDialogService dialogService;

        private readonly ILoadingService loadingService;

        private readonly ISettingService settingService;

        private readonly INetworkClient networkClient;

        private readonly ItemService itemService;

        public AsyncCommand BackCommand { get; }

        public AsyncCommand RecieveCommand { get; }

        public MasterDownloadPageViewModel(
            INavigator navigator,
            IDialogService dialogService,
            ILoadingService loadingService,
            ISettingService settingService,
            INetworkClient networkClient,
            ItemService itemService)
        {
            this.navigator = navigator;
            this.dialogService = dialogService;
            this.loadingService = loadingService;
            this.settingService = settingService;
            this.networkClient = networkClient;
            this.itemService = itemService;

            BackCommand = MakeBusyCommand(Back);
            RecieveCommand = MakeBusyCommand(Recieve);
        }

        private async Task Back()
        {
            await navigator.ForwardAsync("/MenuPage");
        }

        private async Task Recieve()
        {
            var result = await loadingService.WithExecuteAsync("Master download", async () =>
            {
                using (var stream = await itemService.OpenItemAsync().ConfigureAwait(false))
                {
                    return await networkClient.Download(
                        EndPoint.MasterItem(settingService.GetEndPoint()),
                        stream,
                        Definition.DownloadTimeout);
                }
            });

            if (result.Success)
            {
                await dialogService.DisplayInformation("Master download", "Download completed.");

                await Back();
            }
            else
            {
                await dialogService.DisplayNetworkError(result);

                await itemService.DeleteItem();
            }
        }
    }
}
