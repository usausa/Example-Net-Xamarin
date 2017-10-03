namespace Example.FormsApp.Views
{
    using Example.FormsApp.Infrastructure;

    /// <summary>
    ///
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
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
