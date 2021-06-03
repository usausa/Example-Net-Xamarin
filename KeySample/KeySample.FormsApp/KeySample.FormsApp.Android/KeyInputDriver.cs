namespace KeySample.FormsApp.Droid
{
    using Android.App;
    using Android.Views;
    using Android.Widget;

    using KeySample.FormsApp.Input;

    public class KeyInputDriver
    {
        private readonly Activity activity;

        public KeyInputDriver(Activity activity)
        {
            this.activity = activity;
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

            // DEL
            if (e.KeyCode == Keycode.Del)
            {
                if (activity.CurrentFocus is EditText)
                {
                    return false;
                }

                if (e.Action == KeyEventActions.Up)
                {
                    InputManager.Default.Process(KeyCode.Del);
                }

                return true;
            }

            return false;
        }
    }
}
