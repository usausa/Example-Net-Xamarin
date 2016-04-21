namespace Example.Navigation.Plugins
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AttributeMemberCache<T>
        where T : Attribute
    {
        private readonly Dictionary<Type, AttributeMember<T>[]> cache = new Dictionary<Type, AttributeMember<T>[]>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<AttributeMember<T>> GetAttributeMembers(Type type)
        {
            AttributeMember<T>[] members;
            if (cache.TryGetValue(type, out members))
            {
                return members;
            }

            members = AttributeMemberFactory.GetAttributeMembers<T>(type);
            cache[type] = members;
            return members;
        }
    }
}
