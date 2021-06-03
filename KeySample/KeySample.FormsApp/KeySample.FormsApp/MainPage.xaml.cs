namespace KeySample.FormsApp
{
    using KeySample.FormsApp.Shell;

    using Smart.Navigation;

    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as MainPageViewModel)?.Navigator.NotifyAsync(ShellEvent.Back);
            return true;
        }
    }
}
