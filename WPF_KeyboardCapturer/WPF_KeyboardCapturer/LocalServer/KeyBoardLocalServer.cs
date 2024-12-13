using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_KeyboardCapturer.LocalServer
{
    public class KeyBoardLocalServer
    {
        private Socket _socket;
        private Socket _client;

        private bool _connectionLock;

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
                    _connectionLock = false;
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
            if (_connectionLock == true)
            {
                return;
            }

            _connectionLock = true;
            _socket.Listen(100);
            _client = await _socket.AcceptAsync();
        }
    }
}