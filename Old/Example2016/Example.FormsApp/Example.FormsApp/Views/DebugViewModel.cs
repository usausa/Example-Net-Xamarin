namespace Example.FormsApp.Views
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Example.FormsApp.Infrastructure;
    using Example.FormsApp.Models;
    using Example.Windows.Messaging;

    public sealed class DebugViewModel : ViewModelBase, IDisposable
    {
        private static int instance;

        private int counter;

        private bool result;

        private string selecedt;

        public ApplicationState State { get; }

        public IMessenger Messenger { get; }

        /// <summary>
        ///
        /// </summary>
        public bool Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }

        /// <summary>
        ///
        /// </summary>
        public string Selected
        {
            get { return selecedt; }
            set { SetProperty(ref selecedt, value); }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        /// <param name="messenger"></param>
        public DebugViewModel(ApplicationState state, IMessenger messenger)
        {
            instance++;
            System.Diagnostics.Debug.WriteLine("[DEBUG] ++DebugViewModel " + instance);

            State = state;
            Messenger = messenger;
        }

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1821:RemoveEmptyFinalizers", Justification = "Debug")]
        ~DebugViewModel()
        {
            instance--;
            System.Diagnostics.Debug.WriteLine("[DEBUG] --DebugViewModel " + instance);
        }

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "Debug")]
        public void Dispose()
        {
            System.Diagnostics.Debug.WriteLine("[DEBUG] DebugViewModel.Dispose()");
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateToMenu()
        {
            Navigator.Forward(ViewId.Menu);
        }

        public void Message()
        {
            counter++;
            Messenger.Send(MessageId.Debug, "Message test " + counter);
        }

        /// <summary>
        ///
        /// </summary>
        public async void Information()
        {
            await Messenger.DisplayAlert("test", "information", "OK");
        }

        /// <summary>
        ///
        /// </summary>
        public async void Confirm()
        {
            Result = await Messenger.DisplayAlert("test", "information", "OK", "Cancel");
        }

        /// <summary>
        ///
        /// </summary>
        public async void Select()
        {
            var items = Enumerable.Range(1, 3).Select(_ => $"Item-{_}").ToArray();
            Selected = await Messenger.DisplayActionSheet("select", "Cancel", "All", items);
        }

        /// <summary>
        ///
        /// </summary>
        public async void Indicator()
        {
            Messenger.SetBusyindicator(true);

            await Task.Delay(1000);

            Messenger.SetBusyindicator(false);
        }

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", Justification = "Ignore")]
        public void Collect()
        {
            GC.Collect();
        }
    }
}
