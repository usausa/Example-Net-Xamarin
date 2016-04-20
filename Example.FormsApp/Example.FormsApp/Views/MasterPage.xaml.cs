namespace Example.FormsApp.Views
{
    using Xamarin.Forms;

    public partial class MasterPage
    {
        /// <summary>
        ///
        /// </summary>
        public ContentView ContentRegion
        {
            get { return Container; }
        }

        public MasterPage(MasterPageViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
