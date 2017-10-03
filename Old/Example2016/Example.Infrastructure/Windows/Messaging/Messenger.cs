namespace Example.Windows.Messaging
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class Messenger : IMessenger
    {
        public event EventHandler<MessengerEventArgs> Recieved;

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameter"></param>
        public void Send(object message = null, object parameter = null)
        {
            Recieved?.Invoke(this, new MessengerEventArgs(message, parameter));
        }
    }
}
