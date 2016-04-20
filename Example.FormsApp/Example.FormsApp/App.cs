namespace Example.FormsApp
{
    using Example.FormsApp.Device;
    using Example.FormsApp.Infrastructure;
    using Example.FormsApp.Models;
    using Example.FormsApp.Views;
    using Example.FormsApp.Views.Functions;
    using Example.FormsApp.Views.Master;
    using Example.FormsApp.Views.Wizard;
    using Example.Navigation;
    using Example.Navigation.Forms;
    using Example.Windows.Messaging;

    using Ninject;

    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    public class App : Application
    {
        // TODO Dispose ?
        private readonly StandardKernel kernel = new StandardKernel();

        /// <summary>
        ///
        /// </summary>
        /// <param name="device"></param>
        public App(IDevice device)
        {
            kernel.Bind<IDevice>().ToConstant(device);

            kernel.Bind<Calculator>().ToConstant(new Calculator(5));
            kernel.Bind<ApplicationState>().ToSelf().InSingletonScope();

            kernel.Bind<Navigator>().ToSelf().InSingletonScope();
            kernel.Bind<IMessenger>().To<Messenger>().InSingletonScope();

            var masterPage = kernel.Get<MasterPage>();

            var navigator = kernel.Get<Navigator>();
            navigator.Factory = new NinjectNavigatorFactory(kernel);
            navigator.Provider = new ContentViewProvider { Container = masterPage.ContentRegion };

            navigator.AddView(ViewId.Menu, typeof(MenuView));
            navigator.AddView(ViewId.Debug, typeof(DebugView));
            navigator.AddView(ViewId.Calculator, typeof(CalculatorView));
            navigator.AddView(ViewId.Master, typeof(MasterView));
            navigator.AddView(ViewId.Detail, typeof(DetailView));
            navigator.AddView(ViewId.Input1, typeof(Input1View));
            navigator.AddView(ViewId.Input2, typeof(Input2View));
            navigator.AddView(ViewId.Result, typeof(ResultView));

            MainPage = masterPage;

            navigator.Forward(ViewId.Menu);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
