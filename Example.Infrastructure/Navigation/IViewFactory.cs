namespace Example.Navigation
{
    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    public interface IViewFactory
    {
        ContentView CreateView(object id);
    }
}
