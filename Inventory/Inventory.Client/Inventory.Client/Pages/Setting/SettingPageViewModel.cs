namespace Inventory.Client.Pages.Setting
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Inventory.Client.Components;
    using Inventory.Client.Models;

    using Smart.Forms.Components;
    using Smart.Forms.Input;
    using Smart.Forms.Navigation;
    using Smart.Forms.Validation;
    using Smart.Forms.ViewModels;

    public class SettingPageViewModel : ViewModelBase
    {
        private readonly INavigator navigator;

        private readonly IDialogService dialogService;

        private readonly ISettingService settingService;

        public ValidationValue<string> EndPoint { get; } = new ValidationValue<string>();

        public ValidationValue<string> TerminalNo { get; } = new ValidationValue<string>();

        public AsyncCommand BackCommand { get; }

        public ValidationRequest ValidationRequest { get; } = new ValidationRequest();

        public SettingPageViewModel(
            INavigator navigator,
            IDialogService dialogService,
            ISettingService settingService)
        {
            this.navigator = navigator;
            this.dialogService = dialogService;
            this.settingService = settingService;

            EndPoint.Validations.Add(new RegexRule<string>(new Regex("^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$")));
            TerminalNo.Validations.Add(new RegexRule<string>(new Regex("^[0-9]+$")));
            RegisterValidation(EndPoint, TerminalNo);

            var endPoint = settingService.GetEndPoint();
            EndPoint.Value = String.IsNullOrEmpty(endPoint) ? "http://" : endPoint;
            TerminalNo.Value = settingService.GetTerminalNo();

            BackCommand = MakeAsyncCommand(Back);
        }

        private async Task Back()
        {
            if (Validate())
            {
                var endPoint = EndPoint.Value.EndsWith("/", StringComparison.OrdinalIgnoreCase)
                    ? EndPoint.Value
                    : EndPoint.Value + "/";
                var terminalNo = TerminalNo.Value.PadLeft(Length.TerminalNo, '0');
                settingService.SetEndPoint(endPoint);
                settingService.SetTerminalNo(terminalNo);
            }
            else if (!await dialogService.DisplayConfirm("Error", "Cancel input ?"))
            {
                ValidationRequest.RaiseValidationError();
                return;
            }

            await navigator.ForwardAsync("/MenuPage");
        }
    }
}
