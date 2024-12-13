using System.Windows.Forms;

namespace WPF_KeyboardCapturer
{
    public class KeysAdapter
    {
        private Sender _sender;

        public KeysAdapter(Sender sender)
        {
            _sender = sender;
        }

        public void SendKey(Keys key, bool isDown)
        {
            string adapted = key.ToString();

            if (isDown == true)
            {
                _sender.SendDown(adapted);
            }
            else
            {
                _sender.SendUp(adapted);
            }
        }
    }
}