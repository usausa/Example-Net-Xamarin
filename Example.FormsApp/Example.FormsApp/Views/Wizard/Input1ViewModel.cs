namespace Example.FormsApp.Views.Wizard
{
    using Example.FormsApp.Infrastructure;

    public class Input1ViewModel : ViewModelBase
    {
        public override string Title
        {
            get { return "Input1"; }
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateToMenu()
        {
            Navigator.Forward(ViewId.Menu);
        }
    }
}
