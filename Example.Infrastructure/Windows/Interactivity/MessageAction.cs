namespace Example.Windows.Interactivity
{
    using System;
    using System.Reflection;

    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MessageAction<T> : BindableObject, IMessageAction
        where T : BindableObject
    {
        /// <summary>
        ///
        /// </summary>
        public TypeInfo ParameterType { get; }

        /// <summary>
        ///
        /// </summary>
        protected MessageAction()
        {
            ParameterType = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        protected MessageAction(Type type)
        {
            ParameterType = type.GetTypeInfo();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="associatedObject"></param>
        /// <param name="parameter"></param>
        public void Invoke(object associatedObject, object parameter)
        {
            Invoke((T)associatedObject, parameter);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="associatedObject"></param>
        /// <param name="parameter"></param>
        protected abstract void Invoke(T associatedObject, object parameter);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public abstract class MessageAction<TObject, TParameter> : BindableObject, IMessageAction
        where TObject : BindableObject
    {
        /// <summary>
        ///
        /// </summary>
        public TypeInfo ParameterType { get; }

        /// <summary>
        ///
        /// </summary>
        protected MessageAction()
        {
            ParameterType = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        protected MessageAction(Type type)
        {
            ParameterType = type.GetTypeInfo();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="associatedObject"></param>
        /// <param name="parameter"></param>
        public void Invoke(object associatedObject, object parameter)
        {
            Invoke((TObject)associatedObject, (TParameter)parameter);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="associatedObject"></param>
        /// <param name="parameter"></param>
        protected abstract void Invoke(TObject associatedObject, TParameter parameter);
    }
}
