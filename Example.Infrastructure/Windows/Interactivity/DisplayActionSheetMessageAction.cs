namespace Example.Windows.Interactivity
{
    using Example.Windows.Messaging;

    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    public class DisplayActionSheetMessageAction : MessageAction<Page, DisplayActionSheetParameter>
    {
        /// <summary>
        ///
        /// </summary>
        public DisplayActionSheetMessageAction()
            : base(typeof(DisplayActionSheetParameter))
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="associatedObject"></param>
        /// <param name="parameter"></param>
        protected override void Invoke(Page associatedObject, DisplayActionSheetParameter parameter)
        {
            parameter.Result = associatedObject.DisplayActionSheet(parameter.Title, parameter.Cancel, parameter.Destruction, parameter.Buttons);
        }
    }
}
