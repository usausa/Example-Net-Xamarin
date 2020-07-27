namespace NavigationSample.Models.Input
{
    public class NumberInputParameter
    {
        public string Value { get; }

        public int MaxLength { get; }

        public int Scale { get; }

        public bool AllowEmpty { get; }

        public NumberInputParameter(string value, int maxLength, int scale, bool allowEmpty)
        {
            Value = value;
            MaxLength = maxLength;
            Scale = scale;
            AllowEmpty = allowEmpty;
        }
    }
}
