namespace Example.Navigation
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ViewAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public object Id { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        public ViewAttribute(object id)
        {
            Id = id;
        }
    }
}
