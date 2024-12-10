using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_KeyboardCapturer.LocalServer
{
    public class KeyBoardLocalServer
    {
        private Socket _socket;
        private Socket _client;

        private const string IP = "127.0.0.1";
        private const int PORT = 60000;

        public KeyBoardLocalServer() 
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(IP), PORT));

            Connect();
        }

        public void Send(byte[] data)
        {
            if (_client != null)
            {
                try
                {
                    _client.Send(data);
                }
                catch 
                { 
                    Connect();
                }
            }
        }

        private async Task Connect()
        {
            _socket.Listen(100);
            _client = await _socket.AcceptAsync();
        }
    }
}