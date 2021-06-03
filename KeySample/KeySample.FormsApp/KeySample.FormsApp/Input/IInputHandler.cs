namespace KeySample.FormsApp.Input
{
    using Xamarin.Forms;

    public interface IInputHandler
    {
        bool Handle(KeyCode key);

        VisualElement? FindFocused();
    }
}
