namespace Inventory.Client.Pages.Inspection
{
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

    public class Inspection2PageViewModel : DisposableViewModelBase, INavigationAware
    {
        private readonly INavigator navigator;

        private readonly IDialogService dialogService;

        private readonly InspectionService inspectionService;

        private InspectionEntity selected;

        private bool updated;

        public NotificationValue<int> UserId { get; } = new NotificationValue<int>();

        public NotificationValue<string> TerminalNo { get; } = new NotificationValue<string>();

        [Parameter(Direction.Import)]
        public NotificationValue<int> StorageNo { get; } = new NotificationValue<int>();

        public NotificationValue<InspectionStatusEntity> Status { get; } = new NotificationValue<InspectionStatusEntity>();

        public ObservableCollection<InspectionEntity> Entities { get; } = new ObservableCollection<InspectionEntity>();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand NextCommand { get; }

        public AsyncCommand<InspectionEntity> EditCommand { get; }

        public Inspection2PageViewModel(
            INavigator navigator,
            IDialogService dialogService,
            ISettingService settingService,
            InspectionService inspectionService,
            Session session)
        {
            this.navigator = navigator;
            this.dialogService = dialogService;
            this.inspectionService = inspectionService;

            UserId.Value = session.UserId;
            TerminalNo.Value = settingService.GetTerminalNo();

            BackCommand = MakeBusyCommand(Back);
            NextCommand = MakeBusyCommand(Next);
            EditCommand = MakeBusyCommand<InspectionEntity>(Edit);
        }

        public void OnNavigatingTo(NavigationContext context)
        {
            if (context.IsPopBack)
            {
                var value = context.Parameters.GetValueOrDefault<long?>(EditParameter.Value);
                if (value.HasValue)
                {
                    selected.Qty = value.Value;

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
                    var pair = await inspectionService.QueryInspectionAsync(StorageNo.Value);

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
                if (!await dialogService.DisplayConfirm("Inspection", "Discard the edits ?"))
                {
                    return;
                }
            }

            await navigator.ForwardAsync("Inspection1Page");
        }

        private async Task Next()
        {
            Status.Value.IsChecked = true;

            await inspectionService.UpdateAsync(Status.Value, Entities.Reverse());

            await navigator.ForwardAsync("Inspection1Page");
        }

        private async Task Edit(InspectionEntity entity)
        {
            selected = entity;

            var parameters = new NavigationParameters()
                .SetValue(EditParameter.Value, entity.Qty)
                .SetValue(EditParameter.ResetValue, entity.Qty);
            await navigator.PushModelAsync("/Edit/QtyEditPage", parameters);
        }

        private void UpdateSummary()
        {
            var summary = Entities
                .Aggregate(new EntrySummaryView(), (s, e) =>
                {
                    s.DetailCount += 1;
                    s.TotalPrice += e.SalesPrice * e.Qty;
                    s.TotalQty += e.Qty;
                    return s;
                });

            Status.Value.DetailCount = summary.DetailCount;
            Status.Value.TotalPrice = summary.TotalPrice;
            Status.Value.TotalQty = summary.TotalQty;
        }
    }
}
