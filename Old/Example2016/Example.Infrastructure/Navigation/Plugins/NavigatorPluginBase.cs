namespace Example.Navigation.Plugins
{
    /// <summary>
    ///
    /// </summary>
    public abstract class NavigatorPluginBase : INavigatorPlugin
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        public virtual void OnCreate(NavigatorPluginContext context, object view, object target)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        public virtual void OnDispose(NavigatorPluginContext context, object view, object target)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        public virtual void OnNavigateFrom(NavigatorPluginContext context, object view, object target)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        public virtual void OnNavigateTo(NavigatorPluginContext context, object view, object target)
        {
        }
    }
}
