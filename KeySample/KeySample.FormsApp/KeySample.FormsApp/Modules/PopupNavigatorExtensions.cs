namespace KeySample.FormsApp.Modules
{
    using System.Threading.Tasks;

    using KeySample.FormsApp.Input;
    using KeySample.FormsApp.Models.Input;

    using XamarinFormsComponents.Popup;

    public static class PopupNavigatorExtensions
    {
        public static ValueTask<string> InputType1Async(this IPopupNavigator popupNavigator, string title, string value, int maxLength)
        {
            return FocusHelper.WithRestoreFocus(() =>
                popupNavigator.PopupAsync<TextInputParameter, string>(
                    DialogId.PopupType1,
                    new TextInputParameter(title, value, maxLength)));
        }
    }
}
