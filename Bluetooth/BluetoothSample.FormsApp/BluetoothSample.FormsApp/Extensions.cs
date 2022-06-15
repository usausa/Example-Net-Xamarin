namespace BluetoothSample.FormsApp
{
    using System;
    using System.Threading.Tasks;

    using Smart.Navigation;

    public static class Extensions
    {
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
