namespace NavigationSample.Models.Input
{
    using System;

    using Smart.ComponentModel;

    public class NumberInputModel : NotificationObject
    {
        private string text = "0";

        public int MaxLength { get; set; }

        public int Scale { get; set; }

        public bool AllowEmpty { get; set; }

        private int IntegerLength => Scale > 0 ? MaxLength - Scale - 1 : MaxLength;

        public string Text
        {
            get => text;
            set => SetProperty(ref text, String.IsNullOrEmpty(value) ? (AllowEmpty ? string.Empty : "0") : value);
        }

        public string NormalizeText => text.EndsWith(".", StringComparison.InvariantCulture) ? text[..^1] : text;

        public void Clear()
        {
            Text = AllowEmpty ? string.Empty : "0";
        }

        public void Pop()
        {
            Text = text.Length > 1 ? text[..^1] : (AllowEmpty ? string.Empty : "0");
        }

        public void Push(string key)
        {
            if (text.Length + key.Length > MaxLength)
            {
                return;
            }

            if (key == ".")
            {
                if (String.IsNullOrEmpty(text) || (text == "0"))
                {
                    Text = "0.";
                }
                else if (text.IndexOf('.', StringComparison.OrdinalIgnoreCase) < 0)
                {
                    Text = text + ".";
                }
            }
            else
            {
                var index = text.IndexOf(".", StringComparison.Ordinal);
                if (index >= 0)
                {
                    if (text.Length - index <= Scale)
                    {
                        Text = text + key;
                    }
                }
                else
                {
                    if (text == "0")
                    {
                        Text = key;
                    }
                    else if (text.Length + key.Length <= IntegerLength)
                    {
                        Text = text + key;
                    }
                }
            }
        }
    }
}
