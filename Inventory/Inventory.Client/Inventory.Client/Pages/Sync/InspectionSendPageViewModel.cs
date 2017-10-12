namespace Inventory.Client.Pages.Sync
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Inventory.Client.Components;
    using Inventory.Client.Models.Entity;
    using Inventory.Client.Models.Network;
    using Inventory.Client.Services;

    using Smart.Forms.Components;
    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.ViewModels;

    public class InspectionSendPageViewModel : DisposableViewModelBase, INavigationAware
    {
        private readonly INavigator navigator;

        private readonly IDialogService dialogService;

        private readonly ILoadingService loadingService;

        private readonly ISettingService settingService;

        private readonly INetworkClient networkClient;

        private readonly InspectionService inspectionService;

        private readonly Session session;

        private InspectionStatusEntity[] allEntities;

        public ObservableCollection<InspectionStatusEntity> Entities { get; } = new ObservableCollection<InspectionStatusEntity>();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand SendCommand { get; }
        public InspectionSendPageViewModel(
            INavigator navigator,
            IDialogService dialogService,
            ILoadingService loadingService,
            ISettingService settingService,
            INetworkClient networkClient,
            InspectionService inspectionService,
            Session session)
        {
            this.navigator = navigator;
            this.dialogService = dialogService;
            this.loadingService = loadingService;
            this.settingService = settingService;
            this.networkClient = networkClient;
            this.inspectionService = inspectionService;
            this.session = session;

            BackCommand = MakeBusyCommand(Back);
            SendCommand = MakeBusyCommand(Send, () => Entities.Count > 0).Observe(Entities);
        }

        public void OnNavigatingTo(NavigationContext context)
        {
        }

        public async void OnNavigatedTo(NavigationContext context)
        {
            if (!context.IsPopBack)
            {
                await ExecuteBusyAsync(async () =>
                {
                    allEntities = await inspectionService.QueryInspectionStatusListAsync();

                    foreach (var entity in allEntities.Where(x => x.IsChecked))
                    {
                        Entities.Add(entity);
                    }
                });
            }
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
        }

        private async Task Back()
        {
            await navigator.ForwardAsync("/MenuPage");
        }

        private async Task Send()
        {
            var result = await loadingService.WithExecuteAsync("Data send", async () =>
            {
                NetworkResult ret = null;

                foreach (var entity in allEntities)
                {
                    if (entity.IsChecked)
                    {
                        var pair = await inspectionService.QueryInspectionAsync(entity.StorageNo);

                        var request = new StorageDetailsRequest
                        {
                            Renew = false,
                            StorageNo = entity.StorageNo,
                            UserId = session.UserId,
                            Entries = pair.Entities.Select(x => new StorageDetailsRequestEntry
                            {
                                DetailNo = x.DetailNo,
                                ItemCode = x.ItemCode,
                                ItemName = x.ItemName,
                                SalesPrice = x.SalesPrice,
                                Amount = x.Amount
                            }).ToArray()
                        };

                        ret = await networkClient.Post(
                            EndPoint.StorageDetails(settingService.GetEndPoint()),
                            request,
                            Definition.Timeout);
                        if (!ret.Success)
                        {
                            return ret;
                        }
                    }

                    await inspectionService.DeleteInspectionLisAsynct(entity.StorageNo);
                }

                return ret;
            });

            if (result.Success)
            {
                await dialogService.DisplayInformation("Data send", "Send completed.");

                await Back();
            }
            else
            {
                await dialogService.DisplayNetworkError(result);
            }
        }
    }
}
