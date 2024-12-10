using System.Net.Sockets;
using System.Net;
using System.Text;

namespace RemoteHotkeyCore.InputsController.InternalNetworking;

public class KeyboardClient
{
    private Socket _socket;
    private byte[] _buffer;

    private const string IP = "127.0.0.1";
    private const int PORT = 60000;

    public event Action<byte[]> messageReceived;

    public KeyboardClient()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        ListenLoop();
    }

    private async Task ListenLoop()
    {
        await _socket.ConnectAsync(new IPEndPoint(IPAddress.Parse(IP), PORT));

        while (true)
        {
            _buffer = new byte[_socket.ReceiveBufferSize];
            int receivedDataSize = _socket.Receive(_buffer);
            byte[] formattedBuffer = new byte[receivedDataSize];

            for (int i = 0; i < receivedDataSize; i++)
            {
                formattedBuffer[i] = _buffer[i];
            }

            messageReceived?.Invoke(formattedBuffer);
        }
    }
}