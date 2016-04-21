namespace Example.Navigation.Plugins
{
    /// <summary>
    ///
    /// </summary>
    public interface INavigatorPlugin
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        void OnCreate(NavigatorPluginContext context, object view, object target);

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        void OnDispose(NavigatorPluginContext context, object view, object target);

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        void OnNavigateFrom(NavigatorPluginContext context, object view, object target);

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        void OnNavigateTo(NavigatorPluginContext context, object view, object target);
    }
}
