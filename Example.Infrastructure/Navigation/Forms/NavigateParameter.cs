namespace Example.Navigation.Forms
{
    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    public class NavigateParameter
    {
        public View View { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        public NavigateParameter(View view)
        {
            View = view;
        }
    }
}
