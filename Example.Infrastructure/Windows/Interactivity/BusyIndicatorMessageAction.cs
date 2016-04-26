namespace Example.Windows.Interactivity
{
    using Example.Windows.Messaging;

    using Xamarin.Forms;

    public class BusyIndicatorMessageAction : MessageAction<Page, BusyIndicatorParameter>
    {
        /// <summary>
        ///
        /// </summary>
        public BusyIndicatorMessageAction()
            : base(typeof(BusyIndicatorParameter))
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="associatedObject"></param>
        /// <param name="parameter"></param>
        protected override void Invoke(Page associatedObject, BusyIndicatorParameter parameter)
        {
            associatedObject.IsBusy = parameter.IsBusy;
        }
    }
}
