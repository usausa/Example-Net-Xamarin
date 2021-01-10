﻿namespace NavigationSample.Modules.Shared
{
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class SharedMain2ViewModel : AppViewModelBase
    {
        public static SharedMain2ViewModel DesignInstance => null; // For design

        public NotificationValue<string> No { get; } = new();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand CompleteCommand { get; }

        public SharedMain2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            BackCommand = MakeAsyncCommand(OnNotifyBackAsync);
            CompleteCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));
        }

        public override void OnNavigatedTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                No.Value = context.Parameter.GetNo();
            }
        }

        protected override Task OnNotifyBackAsync()
        {
            return Navigator.ForwardAsync(ViewId.SharedInput, Parameters.MakeNextViewId(ViewId.SharedMain2));
        }
    }
}
