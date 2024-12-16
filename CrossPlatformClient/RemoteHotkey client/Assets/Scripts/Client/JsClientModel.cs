using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Text;
using WebSocketSharp;

public class JsClientModel : IClient
{
    private WebSocket _socket;
    private PackegeParserHelper _parserHelper;

    private string _userName;

    public event Action connected;
    public event Action disconnected;
    public event Action<string> disconnectedWithError;
    public event Action<byte[]> receivedClientDirectedMessage;

    public JsClientModel()
    {
        _parserHelper = new PackegeParserHelper(ClientConstantsHolder.USER_NAME_SEPARATE_CHAR);
    }

    public async UniTask Connect(string ip, string userName)
    {
        _socket = new WebSocket($"ws://{ip}:12345");
        _userName = userName;

        _socket.Connect();
        _socket.Send($"connection:@{userName}#0");

        _socket.OnMessage += OnMessageReceived;
        connected?.Invoke();
    }

    public void Disconnect()
    {
        try
        {
            _socket.Close();
            disconnected?.Invoke();
        }
        catch (Exception ex)
        {
            disconnectedWithError?.Invoke(ex.Message);
        }
    }

    public void Dispose()
    {
        Disconnect();
    }

    public async UniTask SendData(byte packegeMarkCode, byte[] message)
    {
        byte[] rawMessage = _parserHelper.ConstructMessage(packegeMarkCode, message, _userName);
        byte[] resultMessage = Encoding.ASCII.GetBytes("send:").Concat(rawMessage).ToArray();

        _socket.Send(resultMessage);
        await UniTask.Delay(0);
    }

    private void OnMessageReceived(object sender, MessageEventArgs e)
    {
        receivedClientDirectedMessage?.Invoke(_parserHelper.GetMessage(e.RawData));
    }
}