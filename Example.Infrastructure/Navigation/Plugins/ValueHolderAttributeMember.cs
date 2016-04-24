namespace Example.Navigation.Plugins
{
    using System;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueHolderAttributeMember<T> : IAttributeMember<T>
        where T : Attribute
    {
        private readonly FieldInfo fieldInfo;

        private readonly PropertyInfo propertyInfo;

        private readonly PropertyInfo valuePropertyInfo;

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
        /// <param name="valuePropertyInfo"></param>
        /// <param name="attribute"></param>
        public ValueHolderAttributeMember(FieldInfo fieldInfo, PropertyInfo valuePropertyInfo, T attribute)
        {
            this.fieldInfo = fieldInfo;
            this.valuePropertyInfo = valuePropertyInfo;
            Attribute = attribute;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="valuePropertyInfo"></param>
        /// <param name="attribute"></param>
        public ValueHolderAttributeMember(PropertyInfo propertyInfo, PropertyInfo valuePropertyInfo, T attribute)
        {
            this.propertyInfo = propertyInfo;
            this.valuePropertyInfo = valuePropertyInfo;
            Attribute = attribute;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            var holder = fieldInfo != null ? fieldInfo.GetValue(target) : propertyInfo.GetValue(target, null);
            return valuePropertyInfo.GetValue(holder);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object value)
        {
            var holder = fieldInfo != null ? fieldInfo.GetValue(target) : propertyInfo.GetValue(target, null);
            valuePropertyInfo.SetValue(holder, value);
        }
    }
}
