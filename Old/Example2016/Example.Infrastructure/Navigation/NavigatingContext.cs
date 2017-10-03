namespace Example.Navigation
{
    public class NavigatingContext
    {
        public object PreviousViewId { get; internal set; }

        public object PreviousView { get; internal set; }

        public object PreviousTarget { get; internal set; }

        public object ViewId { get; internal set; }

        public object View { get; internal set; }

        public object Target { get; internal set; }

        public NavigatingParameter Parameter { get; } = new NavigatingParameter();
    }
}
