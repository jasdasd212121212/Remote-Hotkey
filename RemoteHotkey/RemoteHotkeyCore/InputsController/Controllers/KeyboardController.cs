using RemoteHotkeyCore.InputsController.Helpers;
using RemoteHotkeyCore.InputsController.InternalNetworking;

namespace RemoteHotkeyCore.InputsController.Controllers;

public class KeyboardController
{
    private KeyboardClient _client;
    private KyeboardDataFormatter _formatter;

    private List<string> _keys = new List<string>();

    public KeyboardController()
    {
        _client = new KeyboardClient();
        _formatter = new KyeboardDataFormatter();

        _client.messageReceived += OnReceiveMessage;
    }

    ~KeyboardController() 
    {
        if (_client != null)
        {
            _client.messageReceived -= OnReceiveMessage;
        }
    }

    public Func<bool> ButtonIsPressed(string key) => () => _keys.Contains(key);

    private void OnReceiveMessage(byte[] message)
    {
        KeyboardDataResponse key = _formatter.DecodeBytes(message);

        if (key.OperationCode == KeyboardOperationCode.KeyPressed)
        {
            if (_keys.Contains(key.Key) == false)
            {
                Console.WriteLine($"Pressed: {key.Key}");
                _keys.Add(key.Key);
            }
        }
        else
        {
            if (_keys.Contains(key.Key) == true)
            {
                Console.WriteLine($"Released: {key.Key}");
                _keys.Remove(key.Key);
            }
        }
    }
}