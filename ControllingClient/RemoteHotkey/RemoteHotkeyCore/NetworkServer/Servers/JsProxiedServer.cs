using RemoteHotkey.Network.Server;
using System.Text;
using WebSocketSharp;

namespace RemoteHotkeyCore.NetworkServer.Servers;

public class JsProxiedServer : IServer
{
    private WebSocket _socket;

    private ServerPackegeHandler _packegeHandler;
    private ServerTechnicalMessageHandler _technicalMessageHandler;

    private string _userName;
    private string _ip;

    public bool IsConnected => _socket.IsAlive;

    public event Action<int> commandPerformReceived;
    public event Action<string> scriptExecutionReuqired;
    public event Action<bool> serverVisibilityChangeRequired;

    public JsProxiedServer(string proxyServerIp, string userName)
    {
        _ip = proxyServerIp;
        _userName = userName;

        _packegeHandler = new ServerPackegeHandler
        (
            (int internalCommandIndex) => { commandPerformReceived?.Invoke(internalCommandIndex); },
            (string script) => { scriptExecutionReuqired?.Invoke(script); },
            (bool isVisible) => { serverVisibilityChangeRequired?.Invoke(isVisible); }
        );

        _technicalMessageHandler = new ServerTechnicalMessageHandler();

        Conncect();
    }

    public void SendMessageToClient(byte[] message)
    {
        if (IsConnected == false)
        {
            Conncect();
        }

        byte[] codedUserName = Encoding.ASCII.GetBytes(_userName + ProxiedServerConfig.USER_NAME_CHARSEPARATE);
        byte[] formattedMessage = new byte[codedUserName.Length + 1];

        formattedMessage[0] = ProxiedServerConfig.CLIENT_MESSAGE_CODE;

        for (int i = 0; i < codedUserName.Length; i++)
        {
            formattedMessage[i + 1] = codedUserName[i];
        }

        formattedMessage = formattedMessage.Concat(message).ToArray();

        string debugMessage = formattedMessage.Length < 1000 ? Encoding.ASCII.GetString(formattedMessage) : $"Message too long. Length: {formattedMessage.Length}";
        Console.WriteLine($"ProxiedServer -> send to clients: {debugMessage}");

        byte[] resultedMessage = Encoding.ASCII.GetBytes("send:").Concat(formattedMessage).ToArray();

        try
        {
            _socket.Send(resultedMessage);
        }
        catch
        {
            _socket.Close();
            Conncect();
        }
    }


    private void OnMesageReceived(object? sender, MessageEventArgs e)
    {
        byte[] buffer = e.RawData;

        if (buffer[0] != ProxiedServerConfig.CLIENT_MESSAGE_CODE)
        {
            HandlePackege(buffer);
        }
    }

    private void HandlePackege(byte[] buffer)
    {
        string text = Encoding.ASCII.GetString(buffer);
        string userName = text.Split(ProxiedServerConfig.USER_NAME_CHARSEPARATE)[0];

        Console.WriteLine($"ProxiedServer -> received bytes count: {buffer.Length}");

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

    public void Stop()
    {
        _socket.Close();   
    }

    private void Conncect()
    {
        _socket = new WebSocket($"ws://{_ip}:12345");

        _socket.Connect();
        _socket.Send($"connection:@{_userName}#0");

        _socket.OnMessage += OnMesageReceived;
    }
}