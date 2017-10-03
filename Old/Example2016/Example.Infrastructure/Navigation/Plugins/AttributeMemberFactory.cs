namespace Example.Navigation.Plugins
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Example.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public static class AttributeMemberFactory
    {
        private static readonly Type ValueHolderType = typeof(IValueHolder<>);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IAttributeMember<T>[] GetAttributeMembers<T>(Type type)
            where T : Attribute
        {
            return type.GetTypeInfo().DeclaredFields.Where(fi => !fi.IsStatic)
                .SelectMany(fi => fi.GetCustomAttributes<T>(), CreateAttributeMember)
                .Union(type.GetTypeInfo().DeclaredProperties.SelectMany(pi => pi.GetCustomAttributes<T>(), CreateAttributeMember))
                .ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type FindValueHolderType(Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces
                .FirstOrDefault(it => it.GetTypeInfo().IsGenericType && it.GetTypeInfo().GetGenericTypeDefinition() == ValueHolderType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fi"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private static IAttributeMember<T> CreateAttributeMember<T>(FieldInfo fi, T attribute)
            where T : Attribute
        {
            var vh = FindValueHolderType(fi.FieldType);
            return vh != null ? (IAttributeMember<T>)new ValueHolderAttributeMember<T>(fi, vh.GetTypeInfo().GetDeclaredProperty("Value"), attribute) : new AttributeMember<T>(fi, attribute);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pi"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private static IAttributeMember<T> CreateAttributeMember<T>(PropertyInfo pi, T attribute)
            where T : Attribute
        {
            var vh = FindValueHolderType(pi.PropertyType);
            return vh != null ? (IAttributeMember<T>)new ValueHolderAttributeMember<T>(pi, vh.GetTypeInfo().GetDeclaredProperty("Value"), attribute) : new AttributeMember<T>(pi, attribute);
        }
    }
}
