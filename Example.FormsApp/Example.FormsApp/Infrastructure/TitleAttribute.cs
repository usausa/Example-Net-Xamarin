namespace Example.FormsApp.Infrastructure
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TitleAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="title"></param>
        public TitleAttribute(string title)
        {
            Title = title;
        }
    }
}
