namespace Example.Navigation.Forms
{
    using Example.Windows.Messaging;

    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    public class MessengerViewProvider : IViewProvider
    {
        private IMessenger Messenger { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="messenger"></param>
        public MessengerViewProvider(IMessenger messenger)
        {
            Messenger = messenger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public object ResolveEventTarget(object view)
        {
            return ((View)view).BindingContext;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        public void ViewSwitch(object view)
        {
            Messenger.Send(null, new NavigateParameter((View)view));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        public void ViewDispose(object view)
        {
            ViewCleanupHelper.Cleanup((View)view);
        }
    }
}
