namespace Example.FormsApp.Views.Master
{
    public partial class DetailView
    {
        public DetailView(DetailView vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
