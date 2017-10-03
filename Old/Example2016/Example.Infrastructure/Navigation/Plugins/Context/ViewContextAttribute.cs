namespace Example.Navigation.Plugins.Context
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ViewContextAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public Type Context { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public ViewContextAttribute()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        public ViewContextAttribute(string key)
        {
            Key = key;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public ViewContextAttribute(Type context)
        {
            Context = context;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="context"></param>
        public ViewContextAttribute(string key, Type context)
        {
            Key = key;
            Context = context;
        }
    }
}
