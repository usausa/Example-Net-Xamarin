namespace Example.FormsApp.Views.Functions
{
    using Example.FormsApp.Infrastructure;

    public class CalculatorViewModel : ViewModelBase
    {
        /// <summary>
        ///
        /// </summary>
        public void NavigateToMenu()
        {
            Navigator.Forward(ViewId.Menu);
        }
    }
}
