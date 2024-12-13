using System;
using WPF_KeyboardCapturer.LocalServer;

namespace WPF_KeyboardCapturer
{
    public class Sender
    {
        private KeyBoardLocalServer _server;
        private PackegeFormatter _formatter;

        public event Action<string, bool> sended;

        public Sender()
        {
            _server = new KeyBoardLocalServer();
            _formatter = new PackegeFormatter();
        }

        public void SendDown(string key)
        {
            _server.Send(_formatter.Pack(key, true));
            sended?.Invoke(key, true);
        }

        public void SendUp(string key) 
        {
            _server.Send(_formatter.Pack(key, false));
            sended?.Invoke(key, false);
        }  
    }
}