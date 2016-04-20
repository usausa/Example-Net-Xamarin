namespace Example.FormsApp.Views
{
    using Example.FormsApp.Infrastructure;

    /// <summary>
    ///
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        public override string Title
        {
            get { return "Menu"; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        public void Navigate(ViewId id)
        {
            Navigator.Forward(id);
        }
    }
}
