namespace Example.Navigation
{
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public static class NavigatorExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="assembly"></param>
        public static void AutoRegister(this Navigator navigator, Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
            {
                foreach (var attr in type.GetTypeInfo().GetCustomAttributes<ViewAttribute>())
                {
                    navigator.AddView(attr.Id, type);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="parameter"></param>
        public static void Notify(this Navigator navigator, object parameter)
        {
            (navigator.CurrentTarget as IViewNotifySupport)?.OnViewNotify(navigator, parameter);
        }
    }
}
