namespace KeySample.FormsApp.Droid
{
    using Android.App;
    using Android.Views;
    using Android.Widget;

    using KeySample.FormsApp.Input;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    using ListView = Android.Widget.ListView;

    public class KeyInputDriver
    {
        private static readonly ConvertEntry[] OtherEntries =
        {
            new(Keycode.Del, KeyCode.Del),
            new(Keycode.Minus, KeyCode.Minus),
            new(Keycode.Period, KeyCode.Period)
        };

        private readonly Activity activity;

        public KeyInputDriver(Activity activity)
        {
            this.activity = activity;

            DependencyService.Register<IInputService, InputService>();
        }

        public bool Process(KeyEvent e)
        {
            // ↑
            if (e.KeyCode == Keycode.DpadUp)
            {
                // MEMO 1 is header
                if ((activity.CurrentFocus is ListView listView) && (listView.SelectedItemPosition > 1))
                {
                    return false;
                }

                if (e.Action == KeyEventActions.Down)
                {
                    InputManager.Default.Process(KeyCode.Up);
                }

                return true;
            }

            // ↓
            if (e.KeyCode == Keycode.DpadDown)
            {
                // MEMO 2 is header and footer
                if ((activity.CurrentFocus is ListView listView) && (listView.SelectedItemPosition < listView.Adapter!.Count - 2))
                {
                    return false;
                }

                if (e.Action == KeyEventActions.Down)
                {
                    InputManager.Default.Process(KeyCode.Down);
                }

                return true;
            }

            // ←
            if (e.KeyCode == Keycode.DpadLeft)
            {
                if (activity.CurrentFocus is EditText editText)
                {
                    // first position
                    if ((editText.SelectionStart == 0) && (editText.SelectionEnd == 0))
                    {
                        return true;
                    }

                    return false;
                }

                return true;
            }

            // →
            if (e.KeyCode == Keycode.DpadRight)
            {
                if (activity.CurrentFocus is EditText editText)
                {
                    // last position
                    var textLength = editText.Text?.Length ?? 0;
                    if ((editText.SelectionStart == textLength) && (editText.SelectionEnd == textLength))
                    {
                        return true;
                    }

                    return false;
                }

                return true;
            }

            // Number
            if ((e.KeyCode >= Keycode.Num0) && (e.KeyCode <= Keycode.Num9))
            {
                if (activity.CurrentFocus is EditText)
                {
                    return false;
                }

                if (e.Action == KeyEventActions.Up)
                {
                    InputManager.Default.Process((KeyCode)((int)KeyCode.Num0 + (e.KeyCode - Keycode.Num0)));
                }

                return true;
            }

            // Function
            if ((e.KeyCode >= Keycode.F1) && (e.KeyCode <= Keycode.F12))
            {
                if (e.Action == KeyEventActions.Up)
                {
                    InputManager.Default.Process((KeyCode)((int)KeyCode.Function1 + (e.KeyCode - Keycode.F1)));
                }

                return true;
            }

            // Others
            foreach (var entry in OtherEntries)
            {
                if (e.KeyCode == entry.AndroidKeycode)
                {
                    if (activity.CurrentFocus is EditText)
                    {
                        return false;
                    }

                    if (e.Action == KeyEventActions.Up)
                    {
                        InputManager.Default.Process(entry.InputKeyCode);
                    }

                    return true;
                }
            }

            return false;
        }

        private class ConvertEntry
        {
            public Keycode AndroidKeycode { get; }

            public KeyCode InputKeyCode { get; }

            public ConvertEntry(Keycode androidKeycode, KeyCode inputKeyCode)
            {
                AndroidKeycode = androidKeycode;
                InputKeyCode = inputKeyCode;
            }
        }

        private class InputService : IInputService
        {
            public int ResolveSelectedPosition(Xamarin.Forms.ListView element)
            {
                var renderer = Platform.GetRenderer(element) as ListViewRenderer;
                return renderer?.Control.SelectedItemPosition - 1 ?? -1;
            }
        }
    }
}
