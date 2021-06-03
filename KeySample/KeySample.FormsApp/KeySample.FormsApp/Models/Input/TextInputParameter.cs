namespace KeySample.FormsApp.Models.Input
{
    public class TextInputParameter
    {
        public string Title { get; }

        public string Value { get; }

        public int MaxLength { get; }

        public TextInputParameter(string title, string value, int maxLength)
        {
            Title = title;
            Value = value;
            MaxLength = maxLength;
        }
    }
}
