namespace KeySample.FormsApp
{
    using System;
    using System.Threading.Tasks;

    using KeySample.FormsApp.Behaviors;
    using KeySample.FormsApp.Helpers;

    using Smart.Navigation;

    using Xamarin.Forms;

    public static class Extensions
    {
        //--------------------------------------------------------------------------------
        // Page
        //--------------------------------------------------------------------------------

        public static void SetDefaultFocus(this Page page)
        {
            var first = default(VisualElement);
            foreach (var visualElement in ElementHelper.EnumerateActive(page))
            {
                if (Focus.GetDefault(visualElement))
                {
                    visualElement.Focus();
                    return;
                }

                first ??= visualElement;
            }

            first?.Focus();
        }

        //--------------------------------------------------------------------------------
        // Navigation
        //--------------------------------------------------------------------------------

        public static async ValueTask PostForwardAsync(this INavigator navigator, object viewId, NavigationParameter? parameter = null)
        {
            if (navigator.Executing)
            {
                async void ExecutingChanged(object sender, EventArgs args)
                {
                    if (!navigator.Executing)
                    {
                        navigator.ExecutingChanged -= ExecutingChanged;
                        await navigator.ForwardAsync(viewId, parameter);
                    }
                }

                navigator.ExecutingChanged += ExecutingChanged;
            }
            else
            {
                await navigator.ForwardAsync(viewId, parameter);
            }
        }

        public static async ValueTask PostActionAsync(this INavigator navigator, Func<Task> task)
        {
            if (navigator.Executing)
            {
                async void ExecutingChanged(object sender, EventArgs args)
                {
                    if (!navigator.Executing)
                    {
                        navigator.ExecutingChanged -= ExecutingChanged;
                        await task();
                    }
                }

                navigator.ExecutingChanged += ExecutingChanged;
            }
            else
            {
                await task();
            }
        }
    }
}
