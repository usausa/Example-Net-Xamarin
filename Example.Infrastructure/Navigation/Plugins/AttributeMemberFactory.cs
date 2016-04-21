﻿namespace Example.Navigation.Plugins
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public static class AttributeMemberFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AttributeMember<T>[] GetAttributeMembers<T>(Type type)
            where T : Attribute
        {
            return type.GetTypeInfo().DeclaredFields.Where(fi => !fi.IsStatic)
                .SelectMany(fi => fi.GetCustomAttributes<T>(), (fi, attr) => new AttributeMember<T>(fi, attr))
                .Union(
                    type.GetTypeInfo().DeclaredProperties.Where(pi => pi.CanWrite)
                        .SelectMany(pi => pi.GetCustomAttributes<T>(), (pi, attr) => new AttributeMember<T>(pi, attr)))
                .ToArray();
        }
    }
}
