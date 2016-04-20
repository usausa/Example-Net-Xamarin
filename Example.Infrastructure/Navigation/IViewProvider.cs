namespace Example.Navigation
{
    public interface IViewProvider
    {
        object ResolveEventTarget(object view);

        void ViewSwitch(object view);

        void ViewDispose(object view);
    }
}
