namespace Example.Windows.Interactivity
{
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public interface IMessageAction
    {
        /// <summary>
        ///
        /// </summary>
        TypeInfo ParameterType { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="associatedObject"></param>
        /// <param name="parameter"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Justification = "Ignore")]
        void Invoke(object associatedObject, object parameter);
    }
}
