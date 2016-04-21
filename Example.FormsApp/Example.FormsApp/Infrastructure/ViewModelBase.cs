﻿namespace Example.FormsApp.Infrastructure
{
    using Example.ComponentModel;
    using Example.Navigation;

    /// <summary>
    ///
    /// </summary>
    public abstract class ViewModelBase : NotificationObject, INavigatorAware
    {
        /// <summary>
        ///
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        ///
        /// </summary>
        public INavigator Navigator { get; set; }
    }
}
