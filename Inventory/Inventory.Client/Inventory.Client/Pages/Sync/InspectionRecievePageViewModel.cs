namespace Inventory.Client.Pages.Sync
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Inventory.Client.Components;
    using Inventory.Client.Models;
    using Inventory.Client.Models.Entity;
    using Inventory.Client.Models.Network;
    using Inventory.Client.Models.View;
    using Inventory.Client.Services;

    using Smart.ComponentModel;
    using Smart.Forms.Components;
    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.ViewModels;

    public class InspectionRecievePageViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigator navigator;

        private readonly IDialogService dialogService;

        private readonly ILoadingService loadingService;

        private readonly ISettingService settingService;

        private readonly INetworkClient networkClient;

        private readonly InspectionService inspectionService;

        private readonly NotificationValue<int> selectedCount = new NotificationValue<int>();

        public ObservableCollection<SelectableItem<StorageResponseEntry>> Items { get; } =
            new ObservableCollection<SelectableItem<StorageResponseEntry>>();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand RecieveCommand { get; }

        public DelegateCommand<SelectableItem<StorageResponseEntry>> SelectCommand { get; }

        public InspectionRecievePageViewModel(
            INavigator navigator,
            IDialogService dialogService,
            ILoadingService loadingService,
            ISettingService settingService,
            INetworkClient networkClient,
            InspectionService inspectionService)
        {
            this.navigator = navigator;
            this.dialogService = dialogService;
            this.loadingService = loadingService;
            this.settingService = settingService;
            this.networkClient = networkClient;
            this.inspectionService = inspectionService;

            BackCommand = MakeAsyncCommand(Back);
            RecieveCommand = MakeAsyncCommand(Recieve, () => selectedCount.Value > 0).Observe(selectedCount);
            SelectCommand = new DelegateCommand<SelectableItem<StorageResponseEntry>>(Select);
        }

        public void OnNavigatingTo(NavigationContext context)
        {
        }

        public async void OnNavigatedTo(NavigationContext context)
        {
            if (!context.IsPopBack)
            {
                var result = await loadingService.WithExecuteAsync(
                    "Storage list",
                    async () => await networkClient.Get<StorageResponse>(
                        EndPoint.StorageList(settingService.GetEndPoint()),
                        Definition.Timeout));

                if (result.Success)
                {
                    foreach (var entry in result.Result.Entries)
                    {
                        Items.Add(new SelectableItem<StorageResponseEntry>(entry));
                    }
                }
                else
                {
                    await dialogService.DisplayNetworkError(result);
                }
            }
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
        }

        private async Task Back()
        {
            await navigator.ForwardAsync("/MenuPage");
        }

        private async Task Recieve()
        {
            var storageNos = Items
                .Where(x => x.IsSelected)
                .Select(x => x.Item.StorageNo)
                .ToList();

            var exists = storageNos
                .Any(x => inspectionService.IsInspectionExists(x));
            if (exists)
            {
                if (!await dialogService.DisplayConfirm("Data recieve", "Data already exists. delete it ?"))
                {
                    return;
                }
            }

            var result = await loadingService.WithExecuteAsync("Data recieve", async () =>
            {
                NetworkResult<StorageDetailsResponse> ret = null;

                foreach (var storageNo in storageNos)
                {
                    ret = await networkClient.Get<StorageDetailsResponse>(
                        EndPoint.StorageDetails(settingService.GetEndPoint(), storageNo),
                        Definition.Timeout);
                    if (!ret.Success)
                    {
                        return ret;
                    }

                    var summary = ret.Result.Entries
                        .Aggregate(new EntrySummaryView(), (s, e) =>
                        {
                            s.DetailCount += 1;
                            s.TotalPrice += e.SalesPrice * e.Qty;
                            s.TotalQty += e.Qty;
                            return s;
                        });

                    // MEMO DetailNo discards it as it will change again when sending
                    await inspectionService.UpdateAsync(
                        new InspectionStatusEntity
                        {
                            StorageNo = ret.Result.StorageNo,
                            EntryUserId = ret.Result.EntryUserId,
                            EntryAt = ret.Result.EntryAt,
                            InspectionUserId = ret.Result.InspectionUserId,
                            InspectionAt = ret.Result.InspectionAt,
                            DetailCount = summary.DetailCount,
                            TotalPrice = summary.TotalPrice,
                            TotalQty = summary.TotalQty,
                            IsChecked = false
                        },
                        ret.Result.Entries.Select((x, i) => new InspectionEntity
                        {
                            ItemCode = x.ItemCode,
                            ItemName = x.ItemName,
                            SalesPrice = x.SalesPrice,
                            Qty = x.Qty,
                            OriginalQty = x.Qty,
                        }));
                }

                return ret;
            });

            if (result.Success)
            {
                await dialogService.DisplayInformation("Data recieve", "Recieve completed.");

                await Back();
            }
            else
            {
                await dialogService.DisplayNetworkError(result);
            }
        }

        private void Select(SelectableItem<StorageResponseEntry> item)
        {
            item.IsSelected = !item.IsSelected;

            selectedCount.Value += item.IsSelected ? 1 : -1;
        }
    }
}
