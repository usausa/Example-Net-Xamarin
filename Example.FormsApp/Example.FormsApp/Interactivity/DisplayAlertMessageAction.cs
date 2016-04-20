namespace Example.FormsApp.Interactivity
{
    using System;

    using Example.Windows.Interactivity;

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
                parameter.Result = associatedObject.DisplayAlert("test", "message", "ok", "cancel");
            }
        }
    }
}
