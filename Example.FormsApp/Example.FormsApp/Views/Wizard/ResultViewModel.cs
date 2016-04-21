namespace Example.FormsApp.Views.Wizard
{
    using Example.FormsApp.Infrastructure;
    using Example.FormsApp.Models;
    using Example.Navigation.Plugins.Context;

    public class ResultViewModel : ViewModelBase
    {
        private WizardContext context;

        public override string Title
        {
            get { return "Result"; }
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
            Navigator.Forward(ViewId.Input2);
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateNext()
        {
            Navigator.Forward(ViewId.Menu);
        }
    }
}
