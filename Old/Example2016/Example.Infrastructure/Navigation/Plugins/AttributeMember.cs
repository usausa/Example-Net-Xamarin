namespace Example.Navigation.Plugins
{
    using System;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AttributeMember<T> : IAttributeMember<T>
        where T : Attribute
    {
        private readonly FieldInfo fieldInfo;

        private readonly PropertyInfo propertyInfo;

        /// <summary>
        ///
        /// </summary>
        public string Name
        {
            get
            {
                return fieldInfo != null ? fieldInfo.Name : propertyInfo.Name;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Type MemberType
        {
            get
            {
                return fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public T Attribute { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="attribute"></param>
        public AttributeMember(FieldInfo fieldInfo, T attribute)
        {
            this.fieldInfo = fieldInfo;
            Attribute = attribute;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="attribute"></param>
        public AttributeMember(PropertyInfo propertyInfo, T attribute)
        {
            this.propertyInfo = propertyInfo;
            Attribute = attribute;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            return fieldInfo != null ? fieldInfo.GetValue(target) : propertyInfo.GetValue(target, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object value)
        {
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(target, value);
            }
            else
            {
                propertyInfo.SetValue(target, value, null);
            }
        }
    }
}
