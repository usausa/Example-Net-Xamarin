namespace Inventory.Client.Models.Stack
{
    using Smart.ComponentModel;

    public class SimpleStack : NotificationObject
    {
        private readonly int maxLength;

        private string buffer = string.Empty;

        public string Value
        {
            get { return buffer; }
            set { SetProperty(ref buffer, value); }
        }

        public SimpleStack(int maxLength)
        {
            this.maxLength = maxLength;
        }

        public void Push(string key)
        {
            if (key == "BS")
            {
                buffer = buffer.Length > 0 ? buffer.Substring(0, buffer.Length - 1) : string.Empty;
            }
            else if (key == "C")
            {
                buffer = string.Empty;
            }
            else if (buffer.Length < maxLength)
            {
                buffer = buffer + key;
            }

            RaisePropertyChanged(nameof(Value));
        }
    }
}
