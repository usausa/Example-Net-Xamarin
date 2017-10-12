namespace Inventory.Client.Pages.Entry
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Inventory.Client.Components;
    using Inventory.Client.Models.Entity;
    using Inventory.Client.Models.View;
    using Inventory.Client.Pages.Edit;
    using Inventory.Client.Services;

    using Smart.ComponentModel;
    using Smart.Forms.Components;
    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.Navigation.Plugins.Parameter;
    using Smart.Forms.ViewModels;

    public class Entry2PageViewModel : DisposableViewModelBase, INavigationAware
    {
        private readonly INavigator navigator;

        private readonly IDialogService dialogService;

        private readonly IBarcodeService barcodeService;

        private readonly ItemService itemService;

        private readonly EntryService entryService;

        private EntryEntity selected;

        private bool updated;

        public NotificationValue<int> UserId { get; } = new NotificationValue<int>();

        public NotificationValue<string> TerminalNo { get; } = new NotificationValue<string>();

        [Parameter(Direction.Import)]
        public NotificationValue<int> StorageNo { get; } = new NotificationValue<int>();

        public NotificationValue<EntryStatusEntity> Status { get; } = new NotificationValue<EntryStatusEntity>();

        public ObservableCollection<EntryEntity> Entities { get; } = new ObservableCollection<EntryEntity>();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand NextCommand { get; }

        public AsyncCommand ScanCommand { get; }

        public AsyncCommand<EntryEntity> EditCommand { get; }

        public Entry2PageViewModel(
            INavigator navigator,
            IDialogService dialogService,
            ISettingService settingService,
            IBarcodeService barcodeService,
            ItemService itemService,
            EntryService entryService,
            Session session)
        {
            this.navigator = navigator;
            this.dialogService = dialogService;
            this.barcodeService = barcodeService;
            this.itemService = itemService;
            this.entryService = entryService;

            UserId.Value = session.UserId;
            TerminalNo.Value = settingService.GetTerminalNo();

            BackCommand = MakeBusyCommand(Back);
            NextCommand = MakeBusyCommand(Next);
            ScanCommand = MakeBusyCommand(Scan);
            EditCommand = MakeBusyCommand<EntryEntity>(Edit);
        }

        public void OnNavigatingTo(NavigationContext context)
        {
            if (context.IsPopBack)
            {
                var value = context.Parameters.GetValueOrDefault<long?>(EditParameter.Value);
                if (value.HasValue)
                {
                    selected.Amount = value.Value;

                    updated = true;

                    UpdateSummary();

                    selected = null;
                }
            }
        }

        public async void OnNavigatedTo(NavigationContext context)
        {
            if (!context.IsPopBack)
            {
                await ExecuteBusyAsync(async () =>
                {
                    var pair = await entryService.QueryEntryAsync(StorageNo.Value);

                    Status.Value = pair.Status;

                    foreach (var entity in pair.Entities)
                    {
                        Entities.Insert(0, entity);
                    }
                });

                UpdateSummary();
            }
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
        }

        private async Task Back()
        {
            if (updated)
            {
                if (!await dialogService.DisplayConfirm("棚入力", "編集を破棄しますか？"))
                {
                    return;
                }
            }

            await navigator.ForwardAsync("Entry1Page");
        }

        private async Task Next()
        {
            await entryService.UpdateAsync(Status.Value, Entities.Reverse());

            await navigator.ForwardAsync("Entry1Page");
        }

        private async Task Scan()
        {
            var code = await barcodeService.ScanAsync();
            if (String.IsNullOrEmpty(code))
            {
                return;
            }

            var item = await itemService.FindItemAsync(code);
            if (item == null)
            {
                return;
            }

            // TODO 本物
            if ((Entities.Count > 0) && (Entities[0].ItemCode == code))
            {
                Entities[0].Amount++;
            }
            else
            {
                var entry = new EntryEntity
                {
                    DetailNo = Entities.Count + 1,
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    SalesPrice = item.SalesPrice,
                    Amount = 1
                };

                Entities.Insert(0, entry);
            }

            updated = true;

            UpdateSummary();
        }

        private async Task Edit(EntryEntity entity)
        {
            selected = entity;

            var parameters = new NavigationParameters()
                .SetValue(EditParameter.Value, entity.Amount)
                .SetValue(EditParameter.ResetValue, entity.Amount);
            await navigator.PushModelAsync("/Edit/AmountEditPage", parameters);
        }

        private void UpdateSummary()
        {
            var summary = Entities
                .Aggregate(new EntrySummaryView(), (s, e) =>
                {
                    s.DetailCount += 1;
                    s.TotalPrice += e.SalesPrice * e.Amount;
                    s.TotalAmount += e.Amount;
                    return s;
                });

            Status.Value.DetailCount = summary.DetailCount;
            Status.Value.TotalPrice = summary.TotalPrice;
            Status.Value.TotalAmount = summary.TotalAmount;
        }
    }
}
