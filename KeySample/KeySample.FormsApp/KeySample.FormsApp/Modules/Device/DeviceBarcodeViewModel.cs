namespace KeySample.FormsApp.Modules.Device
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using KeySample.FormsApp.Components.Barcode;
    using KeySample.FormsApp.Models.Entry;

    using Smart.Navigation;

    public class DeviceBarcodeViewModel : AppViewModelBase
    {
        public IAttachableBarcodeReader BarcodeReader { get; }

        public EntryModel Input1 { get; }
        public EntryModel Input2 { get; }
        public EntryModel Input3 { get; }

        public DeviceBarcodeViewModel(
            ApplicationState applicationState,
            IAttachableBarcodeReader barcodeReader)
            : base(applicationState)
        {
            BarcodeReader = barcodeReader;

            Input1 = new EntryModel(MakeDelegateCommand<EntryCompleteEvent>(Input1Complete));
            Input2 = new EntryModel(MakeDelegateCommand<EntryCompleteEvent>(Input2Complete));
            Input3 = new EntryModel(MakeDelegateCommand<EntryCompleteEvent>(Input3Complete));
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();

        private void Input1Complete(EntryCompleteEvent ice)
        {
            Debug.WriteLine($"**** Input1 completed {Input1.Text}");
        }

        private void Input2Complete(EntryCompleteEvent ice)
        {
            Debug.WriteLine($"**** Input2 completed {Input2.Text}");
        }

        private void Input3Complete(EntryCompleteEvent ice)
        {
            Debug.WriteLine($"**** Input3 completed {Input3.Text}");

            Input1.FocusRequest();
        }
    }
}
