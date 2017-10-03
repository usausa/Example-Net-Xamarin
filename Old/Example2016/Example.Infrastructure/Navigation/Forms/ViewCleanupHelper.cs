namespace Example.Navigation.Forms
{
    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    public static class ViewCleanupHelper
    {
        public static void Cleanup(View view)
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
