namespace Example.Navigation.Plugins
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class NavigatorPluginContext
    {
        private Dictionary<Type, object> store;

        public INavigatorFactory Factory { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        public NavigatorPluginContext(INavigatorFactory factory)
        {
            Factory = factory;
        }

        /// <summary>
        ///
        /// </summary>
        private void Prepare()
        {
            if (store == null)
            {
                store = new Dictionary<Type, object>();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void Save<T>(Type type, T value)
        {
            Prepare();
            store[type] = value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public T Load<T>(Type type)
        {
            Prepare();
            return (T)store[type];
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T LoadOr<T>(Type type, T defaultValue)
        {
            if (store == null)
            {
                return defaultValue;
            }

            Prepare();

            object value;
            return store.TryGetValue(type, out value) ? (T)value : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="defaultValueFactory"></param>
        /// <returns></returns>
        public T LoadOr<T>(Type type, Func<T> defaultValueFactory)
        {
            if (store == null)
            {
                return defaultValueFactory();
            }

            Prepare();

            object value;
            return store.TryGetValue(type, out value) ? (T)value : defaultValueFactory();
        }
    }
}
