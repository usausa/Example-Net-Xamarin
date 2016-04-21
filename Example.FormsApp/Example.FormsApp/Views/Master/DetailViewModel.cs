namespace Example.FormsApp.Views.Master
{
    using Example.FormsApp.Infrastructure;

    public class DetailViewModel : ViewModelBase
    {
        public override string Title
        {
            get { return "Detail"; }
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateBack()
        {
            Navigator.Forward(ViewId.Master);
        }
    }
}