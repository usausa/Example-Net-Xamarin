namespace Example.Navigation.Forms
{
    using Example.Windows.Interactivity;

    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    public class ViewNavigateMessageAction : MessageAction<ContentView, NavigateParameter>
    {
        /// <summary>
        ///
        /// </summary>
        public ViewNavigateMessageAction()
            : base(typeof(NavigateParameter))
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="associatedObject"></param>
        /// <param name="parameter"></param>
        protected override void Invoke(ContentView associatedObject, NavigateParameter parameter)
        {
            associatedObject.Content = parameter.View;
        }
    }
}
