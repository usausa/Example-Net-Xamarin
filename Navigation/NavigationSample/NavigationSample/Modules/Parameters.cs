namespace NavigationSample.Modules
{
    using Smart.Navigation;

    public static class Parameters
    {
        private const string NextViewId = nameof(NextViewId);

        private const string No = nameof(No);

        public static NavigationParameter MakeNextViewId(ViewId viewId) =>
            new NavigationParameter().SetValue(NextViewId, viewId);

        public static ViewId GetNextViewId(this INavigationParameter parameter) =>
            parameter.GetValue<ViewId>(NextViewId);

        public static NavigationParameter WithNo(this NavigationParameter parameter, string no) =>
            parameter.SetValue(No, no);

        public static string GetNo(this INavigationParameter parameter) =>
            parameter.GetValue<string>(No);
    }
}
