namespace Example.Navigation.Plugins.Context
{
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class ViewContextPlugin : NavigatorPluginBase
    {
        private class Reference
        {
            public object Context { get; set; }

            public int Counter { get; set; }
        }

        private readonly AttributeMemberCache<ViewContextAttribute> cache = new AttributeMemberCache<ViewContextAttribute>();

        private readonly Dictionary<string, Reference> store = new Dictionary<string, Reference>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        public override void OnCreate(NavigatorPluginContext context, object view, object target)
        {
            foreach (var member in cache.GetAttributeMembers(target.GetType()))
            {
                var key = member.Attribute.Key ?? member.MemberType.FullName;

                Reference reference;
                if (!store.TryGetValue(key, out reference))
                {
                    reference = new Reference
                    {
                        Context = context.Factory.Create(member.Attribute.Context ?? member.MemberType)
                    };

                    (reference.Context as IViewContextSupport)?.Initilize();

                    store[key] = reference;
                }

                reference.Counter++;

                member.SetValue(target, reference.Context);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        public override void OnDispose(NavigatorPluginContext context, object view, object target)
        {
            foreach (var member in cache.GetAttributeMembers(target.GetType()))
            {
                var key = member.Attribute.Key ?? member.MemberType.FullName;

                Reference reference;
                if (!store.TryGetValue(key, out reference))
                {
                    continue;
                }

                reference.Counter--;
                if (reference.Counter != 0)
                {
                    continue;
                }

                (reference.Context as IViewContextSupport)?.Cleanup();

                store.Remove(key);
            }
        }
    }
}
