namespace Example.FormsApp.Models
{
    using Example.ComponentModel;
    using Example.Navigation.Plugins.Context;

    /// <summary>
    ///
    /// </summary>
    public class WizardContext : NotificationObject, IViewContextSupport
    {
        private string value1;

        private string value2;

        /// <summary>
        ///
        /// </summary>
        public string Value1
        {
            get { return value1; }
            set { SetProperty(ref value1, value); }
        }

        /// <summary>
        ///
        /// </summary>
        public string Value2
        {
            get { return value2; }
            set { SetProperty(ref value2, value); }
        }

        /// <summary>
        ///
        /// </summary>
        public void Initilize()
        {
            System.Diagnostics.Debug.WriteLine("[DEBUG] WizardContext.Initilize()");
        }

        /// <summary>
        ///
        /// </summary>
        public void Cleanup()
        {
            System.Diagnostics.Debug.WriteLine("[DEBUG] WizardContext.Cleanup()");
        }
    }
}
