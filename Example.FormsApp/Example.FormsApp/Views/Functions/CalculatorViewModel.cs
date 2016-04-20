namespace Example.FormsApp.Views.Functions
{
    using Example.FormsApp.Infrastructure;

    public class CalculatorViewModel : ViewModelBase
    {
        public override string Title
        {
            get { return "Calculator"; }
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
