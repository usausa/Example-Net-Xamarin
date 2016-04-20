namespace Example.FormsApp.Models
{
    using System;
    using System.Globalization;

    using Example.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public class Calculator : NotificationObject
    {
        private const string Cleared = "0";

        private readonly int maxLength;

        private string buffer = Cleared;

        public int Value
        {
            get { return Int32.Parse(buffer, CultureInfo.InvariantCulture); }
            set { SetProperty(ref buffer, value.ToString(CultureInfo.InvariantCulture)); }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxLength"></param>
        public Calculator(int maxLength)
        {
            this.maxLength = maxLength;
        }

        /// <summary>
        ///
        /// </summary>
        public void Reset()
        {
            Push("AC");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        public void Push(string key)
        {
            if (key == "C")
            {
                buffer = buffer.Length > 1 ? buffer.Substring(0, buffer.Length - 1) : Cleared;
            }
            else if (key == "AC")
            {
                buffer = Cleared;
            }
            else if (buffer.Length > maxLength)
            {
                return;
            }
            else if ((buffer == Cleared) && (key.Length > 0) && (key[0] == '0'))
            {
                return;
            }
            else if (buffer == Cleared)
            {
                buffer = key.Substring(0, Math.Min(key.Length, maxLength));
            }
            else
            {
                buffer = buffer + key.Substring(0, Math.Min(key.Length, maxLength - buffer.Length));
            }

            RaisePropertyChanged(nameof(Value));
        }
    }
}
