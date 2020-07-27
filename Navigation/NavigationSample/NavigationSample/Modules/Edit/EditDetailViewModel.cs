namespace NavigationSample.Modules.Edit
{
    using System;
    using System.Threading.Tasks;

    using NavigationSample.Models.Entity;
    using NavigationSample.Services;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class EditDetailViewModel : AppViewModelBase
    {
        public static EditDetailViewModel DesignInstance { get; } = null; // For design

        private readonly DataService dataService;

        private DataEntity entity;

        public NotificationValue<bool> IsUpdate { get; } = new NotificationValue<bool>();

        public NotificationValue<string> Name { get; } = new NotificationValue<string>();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand UpdateCommand { get; }

        public EditDetailViewModel(
            ApplicationState applicationState,
            DataService dataService)
            : base(applicationState)
        {
            this.dataService = dataService;

            BackCommand = MakeAsyncCommand(OnNotifyBackAsync);
            UpdateCommand = MakeAsyncCommand(ExecuteUpdate, () => !String.IsNullOrEmpty(Name.Value))
                .Observe(Name);
        }

        public override void OnNavigatingTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                IsUpdate.Value = Equals(context.ToId, ViewId.EditDetailUpdate);
                if (IsUpdate.Value)
                {
                    entity = context.Parameter.GetValue<DataEntity>();
                    Name.Value = entity.Name;
                }
            }
        }
        protected override Task OnNotifyBackAsync()
        {
            return Navigator.ForwardAsync(ViewId.EditList);
        }

        private Task ExecuteUpdate()
        {
            if (IsUpdate.Value)
            {
                entity.Name = Name.Value;
                dataService.UpdateSample(entity);
            }
            else
            {
                dataService.InsertSample(Name.Value);
            }

            return Navigator.ForwardAsync(ViewId.EditList);
        }
    }
}
