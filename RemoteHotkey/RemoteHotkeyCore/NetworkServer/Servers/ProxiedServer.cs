using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteHotkey.Network.Server;

public class ProxiedServer : IServer
{
    private Socket _socket;
    private Socket _sendSocket;

    private IPEndPoint _endPoint;

    private ServerPackegeHandler _packegeHandler;
    private ServerTechnicalMessageHandler _technicalMessageHandler;

    private string _userName;
    private bool _sendSocketConnected;

    public bool IsConnected { get; private set; }

    public event Action<int> commandPerformReceived;
    public event Action<string> scriptExecutionReuqired;
    public event Action<bool> serverVisibilityChangeRequired;

    public ProxiedServer(string proxyServerIp, string userName) 
    {
        _packegeHandler = new ServerPackegeHandler
        (
            (int internalCommandIndex) => { commandPerformReceived?.Invoke(internalCommandIndex); },
            (string script) => { scriptExecutionReuqired?.Invoke(script); },
            (bool isVisible) => { serverVisibilityChangeRequired?.Invoke(isVisible); }
        );

        _technicalMessageHandler = new ServerTechnicalMessageHandler();

        _endPoint = new IPEndPoint(IPAddress.Parse(proxyServerIp), 12345);

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        _userName = userName;

        HadleLoop();
    }

    ~ProxiedServer() 
    {
        Disconnect();
    }

    public async void SendMessageToClient(byte[] message)
    {
        if (_sendSocketConnected == false)
        {
            await _sendSocket.ConnectAsync(_endPoint);
            _sendSocketConnected = true;
        }

        byte[] codedUserName = Encoding.ASCII.GetBytes(_userName + ProxiedServerConfig.USER_NAME_CHARSEPARATE);
        byte[] formattedMessage = new byte[codedUserName.Length + message.Length + 1];

        formattedMessage[0] = ProxiedServerConfig.CLIENT_MESSAGE_CODE;

        for (int i = 0; i < codedUserName.Length; i++)
        {
            formattedMessage[i + 1] = codedUserName[i];
        }

        for (int i = formattedMessage.Length; i < formattedMessage.Length; i++)
        {
            formattedMessage[i] = message[i - codedUserName.Length];
        }

        Console.WriteLine($"ProxiedServer -> send to clients: {Encoding.ASCII.GetString(formattedMessage)}");

        try
        {
            await _sendSocket.SendAsync(formattedMessage);
        }
        catch 
        { 
            _sendSocket.Close();
            _sendSocketConnected = false;
            _sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }

    public void Disconnect()
    {
        IsConnected = false;

        _socket.Shutdown(SocketShutdown.Both);
        _socket.Dispose();
    }

    private async void HadleLoop()
    {
        if (await TryConnect() == false)
        {
            return;
        }

        while (true)
        {
            try
            {
                byte[] buffer = new byte[_socket.ReceiveBufferSize];
                int receivedBytesCount = await _socket.ReceiveAsync(buffer);
                
                if (buffer[0] != ProxiedServerConfig.CLIENT_MESSAGE_CODE)
                {
                    HandlePackege(buffer, receivedBytesCount);
                }
            }
            catch { }
        }
    }

    private async Task<bool> TryConnect()
    {
        try
        {
            await _socket.ConnectAsync(_endPoint);
            Console.WriteLine("ProxiedServer -> succesfully connect");

            IsConnected = true;

            return true;
        }
        catch
        {
            Console.WriteLine("ProxiedServer -> connection failure");

            Console.Read();
            return false;
        }
    }

    private void HandlePackege(byte[] buffer, int receivedBytesCount)
    {
        buffer = ThruncastBuffer(buffer, receivedBytesCount);

        string text = Encoding.ASCII.GetString(buffer);
        string userName = text.Split(ProxiedServerConfig.USER_NAME_CHARSEPARATE)[0];

        Console.WriteLine($"ProxiedServer -> received bytes count: {receivedBytesCount}");

        if (userName.Trim().Substring(1, userName.Length - 1).Trim() == _userName.Trim())
        {
            _packegeHandler.HandlePackege(PreparePackege(text, buffer));
        }
        else
        {
            Console.WriteLine($"Reject user name: {userName}; SelfName: {_userName}");
            _technicalMessageHandler.HandlePackege(buffer);
        }
    }

    private byte[] PreparePackege(string text, byte[] buffer)
    {
        string erasedText = text.Split(ProxiedServerConfig.USER_NAME_CHARSEPARATE)[1];

        byte[] codedErasedText = Encoding.ASCII.GetBytes(erasedText);
        byte[] preparedPackege = new byte[codedErasedText.Length + 1];

        for (int i = 1; i < preparedPackege.Length; i++)
        {
            preparedPackege[i] = codedErasedText[i - 1];
        }

        preparedPackege[0] = buffer[0];

        return preparedPackege;
    }

    private byte[] ThruncastBuffer(byte[] source, int bytesCount)
    {
        byte[] result = new byte[bytesCount];   

        for (int i = 0; i < bytesCount; i++)
        {
            result[i] = source[i];
        }

        return result;
    }

    public void Stop()
    {
        Disconnect();
    }
}