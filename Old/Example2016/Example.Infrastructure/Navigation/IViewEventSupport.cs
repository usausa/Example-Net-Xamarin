namespace Example.Navigation
{
    /// <summary>
    ///
    /// </summary>
    public interface IViewEventSupport
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        void OnViewNavigateFrom(NavigatingContext context);

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        void OnViewNavigateTo(NavigatingContext context);
    }
}
