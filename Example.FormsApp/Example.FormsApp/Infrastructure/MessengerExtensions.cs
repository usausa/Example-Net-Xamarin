namespace Example.FormsApp.Infrastructure
{
    using System.Threading.Tasks;

    using Example.FormsApp.Interactivity;

    using Example.Windows.Messaging;

    /// <summary>
    ///
    /// </summary>
    public static class MessengerExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="messenger"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public static Task DisplayAlert(this IMessenger messenger, string title, string message, string cancel)
        {
            var parameter = new DisplayAlertParameter { Title = title, Message = message, Cancel = cancel };
            messenger.Send(null, parameter);
            return parameter.Result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="messenger"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="accept"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public static Task<bool> DisplayAlert(this IMessenger messenger, string title, string message, string accept, string cancel)
        {
            var parameter = new DisplayAlertParameter { Title = title, Message = message, Accept = accept, Cancel = cancel };
            messenger.Send(null, parameter);
            return parameter.Result;
        }
    }
}
