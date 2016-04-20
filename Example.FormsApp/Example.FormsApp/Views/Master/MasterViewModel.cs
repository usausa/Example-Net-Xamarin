namespace Example.FormsApp.Views.Master
{
    using Example.FormsApp.Infrastructure;

    public class MasterViewModel : ViewModelBase
    {
        public override string Title
        {
            get { return "Select"; }
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
