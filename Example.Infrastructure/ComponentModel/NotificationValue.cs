namespace Example.ComponentModel
{
    using System.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class NotificationValue<T> : IValueHolder<T>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private T storage;

        /// <summary>
        ///
        /// </summary>
        public T Value
        {
            get
            {
                return storage;
            }
            set
            {
                if (Equals(storage, value))
                {
                    return;
                }

                storage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }
    }
}
