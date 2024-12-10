using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteHotkey.Network.Server;

public class Server : IServer
{
    private Socket _socket;
    private Socket _currentAcceptedSocket;

    private ServerPackegeHandler _packegeHandler;
    private ServerTechnicalMessageHandler _techicalMessageHendler;

    private byte[] _buffer;

    private bool _isOpen;

    public bool IsConnected => _isOpen;

    public event Action<int> commandPerformReceived;
    public event Action<string> scriptExecutionReuqired;
    public event Action<bool> serverVisibilityChangeRequired;

    public Server()
    {
        _isOpen = true;

        string ip = new ServerIPObtainer().ObtainIP();

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), 12345));

        _packegeHandler = new ServerPackegeHandler
        (
            (int internalCommandIndex) => { commandPerformReceived?.Invoke(internalCommandIndex); },
            (string script) => { scriptExecutionReuqired?.Invoke(script); },
            (bool isVisible) => { serverVisibilityChangeRequired?.Invoke(isVisible); }
        );

        Console.WriteLine($"Server _ip: {ip}");

        ListenSocket();
    }

    ~Server()
    {
        _isOpen = false;

        _socket.Close();
        _socket.Dispose();
    }

    public async void SendMessageToClient(byte[] message)
    {
        byte[] formattedMessage = new byte[message.Length + 1];

        for (int i = 1; i < formattedMessage.Length; i++)
        {
            formattedMessage[i] = message[i - 1];
        }

        formattedMessage[0] = ProxiedServerConfig.CLIENT_MESSAGE_CODE;

        await _currentAcceptedSocket.SendAsync(formattedMessage);
    }

    private async void ListenSocket()
    {
        Console.WriteLine("Socket -> listen started");

        while (_isOpen == true)
        {
            Console.WriteLine("Server listen...");

            _socket.Listen(100);
            Socket accepted = await _socket.AcceptAsync();
            _currentAcceptedSocket = accepted;

            _buffer = new byte[accepted.ReceiveBufferSize];

            try
            {
                int receivedDataSize = accepted.Receive(_buffer);
                byte[] formattedBuffer = new byte[receivedDataSize];

                for (int i = 0; i < receivedDataSize; i++)
                {
                    formattedBuffer[i] = _buffer[i];
                }

                Console.WriteLine($"Server listen -> Byte buffer size: {receivedDataSize}");

                _packegeHandler.HandlePackege(formattedBuffer);
                _techicalMessageHendler.HandlePackege(formattedBuffer);
            }
            catch
            {
                Console.WriteLine("Connection closed");
            }

            await Task.Delay(100);

            accepted.Close();
        }
    }

    public void Stop()
    {
        _socket.Shutdown(SocketShutdown.Both);
        _currentAcceptedSocket.Shutdown(SocketShutdown.Both);

        _socket.Close();
        _currentAcceptedSocket.Close();

        _socket.Dispose();
        _currentAcceptedSocket.Dispose();
    }
}