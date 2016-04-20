namespace Example.Navigation
{
    using System;

    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class NavigatingParameter
    {
        private Dictionary<string, object> values;

        /// <summary>
        ///
        /// </summary>
        private void Prepare()
        {
            if (values == null)
            {
                values = new Dictionary<string, object>();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set<T>(string key, T value)
        {
            Prepare();
            values[key] = value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            Prepare();
            return (T)values[key];
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetOr<T>(string key, T defaultValue)
        {
            if (values == null)
            {
                return defaultValue;
            }

            Prepare();

            object value;
            return values.TryGetValue(key, out value) ? (T)value : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValueFactory"></param>
        /// <returns></returns>
        public T GetOr<T>(string key, Func<T> defaultValueFactory)
        {
            if (values == null)
            {
                return defaultValueFactory();
            }

            Prepare();

            object value;
            return values.TryGetValue(key, out value) ? (T)value : defaultValueFactory();
        }
    }
}
