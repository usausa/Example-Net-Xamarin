namespace Example.FormsApp.Views
{
    using System;

    using Example.FormsApp.Infrastructure;
    using Example.FormsApp.Models;
    using Example.Windows.Messaging;

    public class DebugViewModel : ViewModelBase, IDisposable
    {
        private static int instance;

        private int counter;

        private bool result;

        public ApplicationState State { get; }

        public IMessenger Messenger { get; }

        public bool Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }

        /// <summary>
        ///
        /// </summary>
        public override string Title
        {
            get { return "Debug"; }
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1821:RemoveEmptyFinalizers", Justification = "Ignore")]
        ~DebugViewModel()
        {
            instance--;
            System.Diagnostics.Debug.WriteLine("[DEBUG] --DebugViewModel " + instance);
        }

        /// <summary>
        ///
        /// </summary>
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", Justification = "Ignore")]
        public void Collect()
        {
            GC.Collect();
        }
    }
}
