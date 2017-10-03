namespace Example.Windows.Interactivity
{
    using System;
    using System.Reflection;

    using Xamarin.Forms;

    /// <summary>
    ///
    /// </summary>
    public class CallMethodBehavior : Behavior<Element>
    {
        private static readonly Type[] EmptyTypes = new Type[0];

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "BindableProperty")]
        public static readonly BindableProperty EventNameProperty =
            BindableProperty.Create("EventName", typeof(string), typeof(CallMethodBehavior), null);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "BindableProperty")]
        public static readonly BindableProperty TargetObjectProperty =
            BindableProperty.Create("TargetObject", typeof(object), typeof(CallMethodBehavior), null);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "BindableProperty")]
        public static readonly BindableProperty MethodNameProperty =
            BindableProperty.Create("MethodName", typeof(string), typeof(CallMethodBehavior), null);

        private EventInfo eventInfo;

        private Delegate handler;

        /// <summary>
        ///
        /// </summary>
        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        /// <summary>
        ///
        /// </summary>
        public object TargetObject
        {
            get { return GetValue(TargetObjectProperty); }
            set { SetValue(TargetObjectProperty, value); }
        }

        /// <summary>
        ///
        /// </summary>
        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Element bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.BindingContextChanged += HandleBindingContextChanged;
            BindingContext = bindable.BindingContext;

            eventInfo = bindable.GetType().GetRuntimeEvent(EventName);
            if (eventInfo == null)
            {
                throw new ArgumentException("EventName");
            }

            var methodInfo = typeof(CallMethodBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            handler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);

            eventInfo?.AddMethod.Invoke(bindable, new object[] { handler });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(Element bindable)
        {
            eventInfo?.RemoveMethod.Invoke(bindable, new object[] { handler });

            bindable.BindingContextChanged -= HandleBindingContextChanged;
            BindingContext = null;

            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = ((View)sender).BindingContext;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnusedParameter.Local", Justification = "Ignore")]
        private void OnEvent(object sender, EventArgs e)
        {
            if ((TargetObject == null) || string.IsNullOrEmpty(MethodName))
            {
                return;
            }

            var methodInfo = TargetObject.GetType().GetRuntimeMethod(MethodName, EmptyTypes);
            methodInfo.Invoke(TargetObject, null);
        }
    }
}
