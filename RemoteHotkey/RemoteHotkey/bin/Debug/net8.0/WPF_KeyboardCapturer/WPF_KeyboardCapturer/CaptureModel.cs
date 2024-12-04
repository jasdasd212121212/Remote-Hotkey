namespace WPF_KeyboardCapturer
{
    public class CaptureModel
    {
        private KeyboardHooks _hooks;
        private KeysAdapter _keysAdapter;

        public CaptureModel(KeysAdapter adapter)
        {
            _keysAdapter = adapter;
            _hooks = new KeyboardHooks();

            _hooks.KeyUp += OnKeyUp;
            _hooks.KeyDown += OnKeyDown;
        }

        ~CaptureModel()
        {
            _hooks.KeyUp -= OnKeyUp;
            _hooks.KeyDown -= OnKeyDown;
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _keysAdapter.SendKey(e.KeyCode, true);
        }

        private void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _keysAdapter.SendKey(e.KeyCode, false);
        }
    }
}