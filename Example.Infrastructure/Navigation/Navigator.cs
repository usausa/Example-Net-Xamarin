namespace Example.Navigation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class Navigator
    {
        public event EventHandler<NavigatingEventArgs> Navigating;

        private readonly Dictionary<object, Type> idToViewType = new Dictionary<object, Type>();

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

            var context = new NavigatingContext
            {
                PreviousViewId = CurrentViewId,
                PreviousView = CurrentView,
                ViewId = id
            };

            // From
            if (context.PreviousView != null)
            {
                (Provider.ResolveEventTarget(context.PreviousView) as IViewEventSupport)?.OnViewNavigateFrom(context);
            }

            // Create
            var view = Factory.Create(type);
            var target = Provider.ResolveEventTarget(view);

            context.View = view;
            context.Target = target;

            CurrentViewId = id;
            CurrentView = view;
            CurrentTarget = target;

            // Injection
            var aware = context.Target as INavigatorAware;
            if (aware != null)
            {
                aware.Navigator = this;
            }

            // Event
            Navigating?.Invoke(this, new NavigatingEventArgs { Context = context });

            // Switch
            Provider.ViewSwitch(view);

            // To
            (context.Target as IViewEventSupport)?.OnViewNavigateTo(context);

            // Dispose Old
            if (context.PreviousView != null)
            {
                Provider.ViewDispose(context.PreviousView);
                (context.PreviousView as IDisposable)?.Dispose();
                (context.Target as IDisposable)?.Dispose();
            }
        }
    }
}
