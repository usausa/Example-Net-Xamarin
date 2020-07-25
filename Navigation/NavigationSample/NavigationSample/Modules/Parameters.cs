namespace NavigationSample.Modules
{
    using Smart.Navigation;

    public static class Parameters
    {
        private const string NextViewId = nameof(NextViewId);

        public static NavigationParameter MakeNextViewId(ViewId viewId) =>
            new NavigationParameter().SetValue(NextViewId, viewId);

        public static ViewId GetNextViewId(this INavigationParameter parameter) =>
            parameter.GetValue<ViewId>(NextViewId);
    }
}
