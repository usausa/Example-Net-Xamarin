namespace NavigationSample.Modules.Main
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    using XamarinFormsComponents.Dialogs;
    using XamarinFormsComponents.Popup;

    public class MenuViewModel : AppViewModelBase
    {
        public static MenuViewModel DesignInstance { get; } = null; // For design

        private readonly IDialogs dialogs;

        private readonly IPopupNavigator popupNavigator;

        public AsyncCommand<ViewId> ForwardCommand { get; }

        public AsyncCommand<ViewId> SharedCommand { get; }

        public AsyncCommand ModalCommand { get; }

        public MenuViewModel(
            ApplicationState applicationState,
            IDialogs dialogs,
            IPopupNavigator popupNavigator)
            : base(applicationState)
        {
            this.dialogs = dialogs;
            this.popupNavigator = popupNavigator;

            ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
            SharedCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(ViewId.SharedInput, Parameters.MakeNextViewId(x)));
            ModalCommand = MakeAsyncCommand(ShowModal);
        }

        private async Task ShowModal()
        {
            var result = await popupNavigator.InputNumberAsync(string.Empty, 8);
            if (result != null)
            {
                await dialogs.Information($"result=[{result}]");
            }
        }
    }
}
