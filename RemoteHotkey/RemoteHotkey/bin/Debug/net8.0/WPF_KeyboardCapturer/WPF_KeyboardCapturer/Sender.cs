using System;

namespace WPF_KeyboardCapturer
{
    public class Sender
    {
        public event Action<string, bool> sended;

        public void SendDown(string key)
        {
            sended?.Invoke(key, true);
        }

        public void SendUp(string key) 
        {
            sended?.Invoke(key, false);
        }  
    }
}