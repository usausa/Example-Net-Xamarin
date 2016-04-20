namespace Example.FormsApp.Views
{
    public partial class DebugView
    {
        private static int instance;

        public DebugView(DebugViewModel vm)
        {
            instance++;
            System.Diagnostics.Debug.WriteLine("[DEBUG] ++DebugView " + instance);

            BindingContext = vm;
            InitializeComponent();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1821:RemoveEmptyFinalizers", Justification = "Ignore")]
        ~DebugView()
        {
            instance--;
            System.Diagnostics.Debug.WriteLine("[DEBUG] --DebugView " + instance);
        }
    }
}
