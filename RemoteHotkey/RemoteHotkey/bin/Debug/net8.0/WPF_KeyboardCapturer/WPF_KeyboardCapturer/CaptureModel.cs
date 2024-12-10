using System.Windows.Forms;

namespace WPF_KeyboardCapturer
{
    public class CaptureModel
    {
        private KeysAdapter _keysAdapter;

        public CaptureModel(KeysAdapter adapter)
        {
            _keysAdapter = adapter;
        }

        public void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _keysAdapter.SendKey(e.KeyCode, true);
        }

        public void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _keysAdapter.SendKey(e.KeyCode, false);
        }
    }
}