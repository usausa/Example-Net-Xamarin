namespace KeySample.FormsApp.Modules.Popup
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.Navigation;

    using XamarinFormsComponents.Popup;

    public class PopupMenuViewModel : AppViewModelBase
    {
        private string value = string.Empty;

        public ICommand Popup1Command { get; }

        public PopupMenuViewModel(
            ApplicationState applicationState,
            IPopupNavigator popupNavigator)
            : base(applicationState)
        {
            Popup1Command = MakeAsyncCommand(async () =>
            {
                value = await popupNavigator.InputType1Async("Input", value, 8);
            });
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
