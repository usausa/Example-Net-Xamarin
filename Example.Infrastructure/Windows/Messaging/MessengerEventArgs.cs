namespace Example.Windows.Messaging
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class MessengerEventArgs : EventArgs
    {
        /// <summary>
        ///
        /// </summary>
        public object Message { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public object Parameter { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameter"></param>
        public MessengerEventArgs(object message, object parameter)
        {
            Message = message;
            Parameter = parameter;
        }
    }
}
