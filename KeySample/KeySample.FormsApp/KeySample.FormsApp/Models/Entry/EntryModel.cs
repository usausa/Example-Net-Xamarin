namespace KeySample.FormsApp.Models.Entry
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    using Smart.ComponentModel;

    public class EntryCompleteEvent
    {
        public bool HasError { get; set; }
    }

    public interface IEntryController : INotifyPropertyChanged
    {
        public event EventHandler<EventArgs> FocusRequested;

        public string? Text { get; set; }

        public bool Enable { get; set; }

        public void HandleCompleted(EntryCompleteEvent e);
    }

    public sealed class EntryModel : NotificationObject, IEntryController
    {
        private event EventHandler<EventArgs>? Requested;

        private readonly ICommand? command;

        private string? text;

        private bool enable;

        public string? Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public bool Enable
        {
            get => enable;
            set => SetProperty(ref enable, value);
        }

        public EntryModel()
        {
            enable = true;
        }

        public EntryModel(bool enable)
        {
            this.enable = enable;
        }

        public EntryModel(ICommand command)
        {
            enable = true;
            this.command = command;
        }

        public EntryModel(bool enable, ICommand command)
        {
            this.enable = enable;
            this.command = command;
        }

        public void FocusRequest()
        {
            Requested?.Invoke(this, EventArgs.Empty);
        }

        event EventHandler<EventArgs> IEntryController.FocusRequested
        {
            add => Requested += value;
            remove => Requested -= value;
        }

        void IEntryController.HandleCompleted(EntryCompleteEvent e)
        {
            if ((command is not null) && command.CanExecute(e))
            {
                command.Execute(e);
            }
        }
    }
}
