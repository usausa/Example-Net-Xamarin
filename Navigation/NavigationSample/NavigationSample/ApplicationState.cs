namespace NavigationSample
{
    using Smart.Forms.ViewModels;

    public sealed class ApplicationState : BusyState
    {
        private bool keyboardVisible;

        public bool KeyboardVisible
        {
            get => keyboardVisible;
            set => SetProperty(ref keyboardVisible, value);
        }
    }
}
