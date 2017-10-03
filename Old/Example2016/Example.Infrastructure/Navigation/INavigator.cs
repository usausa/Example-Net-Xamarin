namespace Example.Navigation
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public interface INavigator
    {
        event EventHandler<NavigatingEventArgs> Navigating;

        object CurrentViewId { get; }

        object CurrentView { get; }

        object CurrentTarget { get; }

        void Forward(object id);
    }
}
