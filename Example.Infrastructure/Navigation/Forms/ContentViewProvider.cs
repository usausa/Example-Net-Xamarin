namespace Example.Navigation.Forms
{
    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    public class ContentViewProvider : IViewProvider
    {
        public ContentView Container { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public object ResolveEventTarget(object view)
        {
            return ((View)view).BindingContext;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        public void ViewSwitch(object view)
        {
            Container.Content = (View)view;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        public void ViewDispose(object view)
        {
            Cleanup((View)view);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        private void Cleanup(View view)
        {
            view.Behaviors.Clear();
            view.Triggers.Clear();

            var layout = view as Layout<View>;
            if (layout != null)
            {
                foreach (var child in layout.Children)
                {
                    Cleanup(child);
                }
            }

            var contentView = view as ContentView;
            if (contentView != null)
            {
                Cleanup(contentView.Content);
            }
        }
    }
}
