namespace Example.FormsApp.Views.Wizard
{
    using Example.FormsApp.Infrastructure;
    using Example.FormsApp.Models;
    using Example.Navigation.Plugins.Context;

    public class Input2ViewModel : ViewModelBase
    {
        private WizardContext context;

        public override string Title
        {
            get { return "Input2"; }
        }

        [ViewContext]
        public WizardContext Context
        {
            get { return context; }
            set { SetProperty(ref context, value); }
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateBack()
        {
            Navigator.Forward(ViewId.Input1);
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateNext()
        {
            Navigator.Forward(ViewId.Result);
        }
    }
}
