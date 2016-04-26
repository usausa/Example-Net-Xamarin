namespace Example.FormsApp.Views
{
    using System;
    using System.Reflection;

    using Example.ComponentModel;
    using Example.FormsApp.Infrastructure;
    using Example.Navigation;
    using Example.Windows.Messaging;

    /// <summary>
    ///
    /// </summary>
    public sealed class MasterPageViewModel : NotificationObject, IDisposable
    {
        private readonly INavigator navigator;

        private string title;

        public IMessenger Messenger { get; }

        /// <summary>
        ///
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="messenger"></param>
        public MasterPageViewModel(INavigator navigator, IMessenger messenger)
        {
            this.navigator = navigator;
            Messenger = messenger;

            navigator.Navigating += NavigatorOnNavigating;
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            navigator.Navigating -= NavigatorOnNavigating;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavigatorOnNavigating(object sender, NavigatingEventArgs args)
        {
            var attribute = args.Context.View.GetType().GetTypeInfo().GetCustomAttribute<TitleAttribute>();
            Title = attribute != null ? attribute.Title : "-";
        }
    }
}
