namespace Example.FormsApp.Views.Wizard
{
    using Example.ComponentModel;
    using Example.FormsApp.Infrastructure;
    using Example.FormsApp.Models;
    using Example.Navigation.Plugins.Context;

    public class Input1ViewModel : ViewModelBase
    {
        [ViewContext]
        public NotificationValue<WizardContext> Context { get; } = new NotificationValue<WizardContext>();

        /// <summary>
        ///
        /// </summary>
        public void NavigateBack()
        {
            Navigator.Forward(ViewId.Menu);
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateNext()
        {
            Navigator.Forward(ViewId.Input2);
        }
    }
}
