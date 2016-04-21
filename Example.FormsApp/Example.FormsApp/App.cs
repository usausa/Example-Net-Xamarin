namespace Example.FormsApp
{
    using System.Reflection;

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

            kernel.Bind<DataService>().ToSelf().InSingletonScope();

            kernel.Bind<Calculator>().ToConstant(new Calculator(5));
            kernel.Bind<ApplicationState>().ToSelf().InSingletonScope();

            var navigator = new Navigator(); // { Factory = new NinjectNavigatorFactory(kernel) };
            navigator.AutoRegister(GetType().GetTypeInfo().Assembly);   // slow?
            kernel.Bind<INavigator>().ToConstant(navigator);

            kernel.Bind<IMessenger>().To<Messenger>().InSingletonScope();

            var masterPage = kernel.Get<MasterPage>();

            navigator.Provider = new ContentViewProvider { Container = masterPage.ContentRegion };

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
