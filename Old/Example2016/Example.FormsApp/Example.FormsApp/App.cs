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
            // Model
            kernel.Bind<IDevice>().ToConstant(device);

            kernel.Bind<DataService>().ToSelf().InSingletonScope();

            kernel.Bind<Calculator>().ToConstant(new Calculator(5));
            kernel.Bind<ApplicationState>().ToSelf().InSingletonScope();

            // View
            kernel.Bind<IMessenger>().To<Messenger>().InSingletonScope();

            // Navigator
            var navigator = new Navigator()
            {
                Factory = new NinjectNavigatorFactory(kernel),
                Provider = new MessengerViewProvider(kernel.Get<IMessenger>())
            };
            navigator.AutoRegister(GetType().GetTypeInfo().Assembly);
            kernel.Bind<INavigator>().ToConstant(navigator);

            // MainPage
            MainPage = kernel.Get<MasterPage>();

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
