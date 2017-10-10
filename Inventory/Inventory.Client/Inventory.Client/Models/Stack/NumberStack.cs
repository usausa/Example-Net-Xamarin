namespace Inventory.Client.Models.Stack
{
    using System;
    using System.Globalization;

    using Smart.ComponentModel;

    public class NumberStack : NotificationObject
    {
        private readonly int maxLength;

        private readonly long maxValue;

        private string resetValue = "0";

        private string buffer = "0";

        public long ResetValue
        {
            get => Int64.Parse(resetValue, CultureInfo.InvariantCulture);
            set => SetProperty(ref resetValue, value.ToString(CultureInfo.InvariantCulture));
        }

        public long Value
        {
            get => Int64.Parse(buffer, CultureInfo.InvariantCulture);
            set => SetProperty(ref buffer, value.ToString(CultureInfo.InvariantCulture));
        }

        public bool IncrementEnabled => Value < maxValue;

        public bool DecrementEnabled => Value > 0;

        public NumberStack(int maxLength)
        {
            this.maxLength = maxLength;
            maxValue = (long)Math.Pow(10, maxLength) - 1;
        }

        public void Push(string key)
        {
            if (key == "BS")
            {
                buffer = buffer.Length > 1 ? buffer.Substring(0, buffer.Length - 1) : "0";
            }
            else if (key == "C")
            {
                buffer = "0";
            }
            else if (key == "RST")
            {
                buffer = resetValue;
            }
            else if (key == "INC")
            {
                var value = Value;
                if (value < maxValue)
                {
                    buffer = (value + 1).ToString(CultureInfo.InvariantCulture);
                }
            }
            else if (key == "DEC")
            {
                var value = Value;
                if (value > 0)
                {
                    buffer = (value - 1).ToString(CultureInfo.InvariantCulture);
                }
            }
            else if (buffer == "0")
            {
                if (key != "0")
                {
                    buffer = key;
                }
            }
            else if (buffer.Length < maxLength)
            {
                buffer = buffer + key;
            }

            RaisePropertyChanged(nameof(Value));
            RaisePropertyChanged(nameof(IncrementEnabled));
            RaisePropertyChanged(nameof(DecrementEnabled));
        }
    }
}
