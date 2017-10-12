namespace Inventory.Client.Pages.Inspection
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using Inventory.Client.Models;
    using Inventory.Client.Models.Entity;
    using Inventory.Client.Services;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.Navigation.Plugins.Parameter;
    using Smart.Forms.ViewModels;

    public class Inspection1PageViewModel : DisposableViewModelBase, INavigationAware
    {
        private readonly INavigator navigator;

        private readonly InspectionService inspectionService;

        private readonly NotificationValue<SelectableItem<InspectionStatusEntity>> selected =
            new NotificationValue<SelectableItem<InspectionStatusEntity>>();

        public ObservableCollection<SelectableItem<InspectionStatusEntity>> Items { get; } =
            new ObservableCollection<SelectableItem<InspectionStatusEntity>>();

        [Parameter(Direction.Export)]
        public int StorageNo { get; set; }

        public AsyncCommand BackCommand { get; }

        public AsyncCommand NextCommand { get; }

        public DelegateCommand<SelectableItem<InspectionStatusEntity>> SelectCommand { get; }

        public Inspection1PageViewModel(
            INavigator navigator,
            InspectionService inspectionService)
        {
            this.navigator = navigator;
            this.inspectionService = inspectionService;

            BackCommand = MakeBusyCommand(Back);
            NextCommand = MakeBusyCommand(Next, () => selected.Value != null).Observe(selected);
            SelectCommand = new DelegateCommand<SelectableItem<InspectionStatusEntity>>(Select);
        }

        public void OnNavigatingTo(NavigationContext context)
        {
        }

        public async void OnNavigatedTo(NavigationContext context)
        {
            if (!context.IsPopBack)
            {
                foreach (var entity in await inspectionService.QueryInspectionStatusListAsync())
                {
                    Items.Add(new SelectableItem<InspectionStatusEntity>(entity));
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

        private async Task Next()
        {
            StorageNo = selected.Value.Item.StorageNo;
            await navigator.ForwardAsync("Inspection2Page");
        }

        private void Select(SelectableItem<InspectionStatusEntity> item)
        {
            item.IsSelected = !item.IsSelected;

            if ((selected.Value != null) && (item != selected.Value))
            {
                selected.Value.IsSelected = false;
            }

            selected.Value = item.IsSelected ? item : null;
        }
    }
}
