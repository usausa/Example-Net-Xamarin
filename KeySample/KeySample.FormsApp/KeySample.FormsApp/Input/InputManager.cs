namespace KeySample.FormsApp.Input
{
    using System.Collections.Generic;

    using Xamarin.Forms;

    public class InputManager
    {
        public static InputManager Default { get; } = new();

        private readonly List<IInputHandler> handlers = new();

        public void PushHandler(IInputHandler handler)
        {
            handlers.Add(handler);
        }

        public void PopHandler(IInputHandler handler)
        {
            handlers.Remove(handler);
        }

        public bool Process(KeyCode key)
        {
            return handlers.Count > 0 && handlers[^1].Handle(key);
        }

        public VisualElement? FindFocused()
        {
            return handlers.Count > 0 ? handlers[^1].FindFocused() : null;
        }
    }
}
