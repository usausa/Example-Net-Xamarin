namespace Example.Windows.Interactivity
{
    using System;

    using Example.Windows.Messaging;

    using Xamarin.Forms;

    public class DisplayAlertMessageAction : MessageAction<Page, DisplayAlertParameter>
    {
        /// <summary>
        ///
        /// </summary>
        public DisplayAlertMessageAction()
            : base(typeof(DisplayAlertParameter))
        {
        }

        protected override void Invoke(Page associatedObject, DisplayAlertParameter parameter)
        {
            if (String.IsNullOrEmpty(parameter.Accept))
            {
                parameter.Result = associatedObject.DisplayAlert(parameter.Title, parameter.Message, parameter.Cancel)
                    .ContinueWith(t => false);
            }
            else
            {
                parameter.Result = associatedObject.DisplayAlert(parameter.Title, parameter.Message, parameter.Accept, parameter.Cancel);
            }
        }
    }
}
