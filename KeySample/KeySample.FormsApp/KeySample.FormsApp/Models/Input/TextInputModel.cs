namespace KeySample.FormsApp.Models.Input
{
    using Smart.ComponentModel;

    public class TextInputModel : NotificationObject
    {
        private string text = string.Empty;

        public int MaxLength { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public void Clear()
        {
            Text = string.Empty;
        }

        public void Pop()
        {
            if (text.Length > 0)
            {
                Text = text[..^1];
            }
        }

        public void Push(string key)
        {
            if (text.Length + key.Length > MaxLength)
            {
                return;
            }

            Text = text + key;
        }
    }
}
