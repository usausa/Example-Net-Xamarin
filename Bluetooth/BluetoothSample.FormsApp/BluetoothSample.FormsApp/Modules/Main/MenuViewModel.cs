namespace BluetoothSample.FormsApp.Modules.Main
{
    using System.Windows.Input;

    using BluetoothSample.FormsApp.Components.Dialog;
    using BluetoothSample.FormsApp.Components.Printer;

    public class MenuViewModel : AppViewModelBase
    {
        public ICommand PrintCommand { get; }

        public MenuViewModel(
            ApplicationState applicationState,
            IApplicationDialog dialog,
            IPrinter printer)
            : base(applicationState)
        {
            PrintCommand = MakeAsyncCommand(async () =>
            {
                using (dialog.Loading("printing"))
                {
                    await printer.WriteAsync("test");
                }
            });
        }
    }
}
