namespace Example.Navigation.Plugins
{
    /// <summary>
    ///
    /// </summary>
    public class NavigatorPluginContext
    {
        public INavigatorFactory Factory { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        public NavigatorPluginContext(INavigatorFactory factory)
        {
            Factory = factory;
        }
    }
}
