namespace NavigationSample
{
    using System;
    using System.Threading.Tasks;

    using Smart.Navigation;

    public static class Extensions
    {
        //--------------------------------------------------------------------------------
        // Navigation
        //--------------------------------------------------------------------------------

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
