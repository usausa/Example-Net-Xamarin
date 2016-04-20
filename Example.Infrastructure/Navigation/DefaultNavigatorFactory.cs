namespace Example.Navigation
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class DefaultNavigatorFactory : INavigatorFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Create(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
