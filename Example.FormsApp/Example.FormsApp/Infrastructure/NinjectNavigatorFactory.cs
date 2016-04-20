namespace Example.FormsApp.Infrastructure
{
    using System;

    using Example.Navigation;

    using Ninject;
    using Ninject.Syntax;

    /// <summary>
    ///
    /// </summary>
    public class NinjectNavigatorFactory : INavigatorFactory
    {
        private readonly IResolutionRoot resolver;

        /// <summary>
        ///
        /// </summary>
        /// <param name="resolver"></param>
        public NinjectNavigatorFactory(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Create(Type type)
        {
            return resolver.Get(type);
        }
    }
}
