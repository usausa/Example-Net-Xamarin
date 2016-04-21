namespace Example.Navigation
{
    using System;
    using System.Collections.Generic;

    using Example.Navigation.Plugins;
    using Example.Navigation.Plugins.Context;
    using Example.Navigation.Plugins.Parameter;

    /// <summary>
    ///
    /// </summary>
    public class Navigator : INavigator
    {
        public event EventHandler<NavigatingEventArgs> Navigating;

        private readonly Dictionary<object, Type> idToViewType = new Dictionary<object, Type>();

        public ICollection<INavigatorPlugin> Plugins { get; } =
            new List<INavigatorPlugin> { new ViewParameterPlugin(), new ViewContextPlugin() };

        public INavigatorFactory Factory { get; set; }

        public IViewProvider Provider { get; set; }

        public object CurrentViewId { get; private set; }

        public object CurrentView { get; private set; }

        public object CurrentTarget { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        public void AddView(object id, Type type)
        {
            idToViewType[id] = type;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        public void Forward(object id)
        {
            Type type;
            if (!idToViewType.TryGetValue(id, out type))
            {
                return;
            }

            var navigationContext = new NavigatingContext
            {
                PreviousViewId = CurrentViewId,
                PreviousView = CurrentView,
                PreviousTarget = CurrentTarget,
                ViewId = id
            };

            var pluginContext = new NavigatorPluginContext(Factory);

            // From
            if (navigationContext.PreviousView != null)
            {
                (Provider.ResolveEventTarget(navigationContext.PreviousView) as IViewEventSupport)?.OnViewNavigateFrom(navigationContext);

                foreach (var plugin in Plugins)
                {
                    plugin.OnNavigateFrom(pluginContext, navigationContext.PreviousView, navigationContext.PreviousTarget);
                }
            }

            // Create
            var view = Factory.Create(type);
            var target = Provider.ResolveEventTarget(view);

            foreach (var plugin in Plugins)
            {
                plugin.OnCreate(pluginContext, view, target);
            }

            // Update
            navigationContext.View = view;
            navigationContext.Target = target;

            CurrentViewId = id;
            CurrentView = view;
            CurrentTarget = target;

            // Injection
            var aware = navigationContext.Target as INavigatorAware;
            if (aware != null)
            {
                aware.Navigator = this;
            }

            // Event
            Navigating?.Invoke(this, new NavigatingEventArgs { Context = navigationContext });

            // Switch
            Provider.ViewSwitch(view);

            // To
            foreach (var plugin in Plugins)
            {
                plugin.OnNavigateTo(pluginContext, CurrentView, CurrentTarget);
            }

            (navigationContext.Target as IViewEventSupport)?.OnViewNavigateTo(navigationContext);

            // Dispose Old
            if (navigationContext.PreviousView != null)
            {
                foreach (var plugin in Plugins)
                {
                    plugin.OnDispose(pluginContext, navigationContext.PreviousView, navigationContext.PreviousTarget);
                }

                Provider.ViewDispose(navigationContext.PreviousView);
                (navigationContext.PreviousView as IDisposable)?.Dispose();
                (navigationContext.PreviousTarget as IDisposable)?.Dispose();
            }
        }
    }
}
