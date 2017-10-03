namespace Example.FormsApp.Views.Master
{
    using Example.FormsApp.Infrastructure;

    public class DetailViewModel : ViewModelBase
    {
        /// <summary>
        ///
        /// </summary>
        public void NavigateBack()
        {
            Navigator.Forward(ViewId.Master);
        }
    }
}