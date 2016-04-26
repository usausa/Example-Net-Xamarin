namespace Example.Windows.Messaging
{
    using System.Threading.Tasks;

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
        /// <param name="cancel"></param>
        /// <param name="destruction"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public static Task<string> DisplayActionSheet(this IMessenger messenger, string title, string cancel, string destruction, params string[] buttons)
        {
            var parameter = new DisplayActionSheetParameter { Title = title, Cancel = cancel, Destruction = destruction, Buttons = buttons };
            messenger.Send(null, parameter);
            return parameter.Result;
        }

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

        /// <summary>
        ///
        /// </summary>
        /// <param name="messenger"></param>
        /// <param name="isBusy"></param>
        public static void SetBusyindicator(this IMessenger messenger, bool isBusy)
        {
            messenger.Send(null, isBusy ? BusyIndicatorParameter.True : BusyIndicatorParameter.False);
        }
    }
}
