namespace KeySample.FormsApp.Droid.Helpers
{
    using Android.Content;
    using Android.Views;
    using Android.Views.InputMethods;

    public static class KeyboardExtensions
    {
        internal static void HideKeyboard(this View inputView)
        {
            using var imm = (InputMethodManager)inputView.Context!.GetSystemService(Context.InputMethodService)!;
            imm.HideSoftInputFromWindow(inputView.WindowToken, HideSoftInputFlags.None);
        }
    }
}
