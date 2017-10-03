namespace Example.Navigation.Plugins.Parameter
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ViewParameterAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public Direction Direction { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public ViewParameterAttribute()
            : this(Direction.Both)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public ViewParameterAttribute(string name)
            : this(Direction.Both, name)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="direction"></param>
        public ViewParameterAttribute(Direction direction)
        {
            Direction = direction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="name"></param>
        public ViewParameterAttribute(Direction direction, string name)
        {
            Direction = direction;
            Name = name;
        }
    }
}
