namespace KeySample.FormsApp.Input
{
    using System;
    using System.Threading.Tasks;

    public static class FocusHelper
    {
        public static async ValueTask<T> WithRestoreFocus<T>(Func<ValueTask<T>> func)
        {
            var focused = InputManager.Default.FindFocused();

            var result = await func();

            if (focused is not null)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => focused.Focus());
            }

            return result;
        }

        public static async ValueTask WithRestoreFocus(Func<ValueTask> func)
        {
            var focused = InputManager.Default.FindFocused();

            await func();

            if (focused is not null)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => focused.Focus());
            }
        }
    }
}
