using RemoteHotkeyCore.InputsController.Helpers;
using RemoteHotkeyCore.InputsController.InternalNetworking;
using RemoteHotkeyCore.InputsController.KeyboardEmulator;

namespace RemoteHotkeyCore.InputsController.Controllers;

public class KeyboardController
{
    private KeyboardClient _client;
    private KyeboardDataFormatter _formatter;
    private KeyboardLowLevelEmulator _keyboardEmulator;

    private List<string> _keys = new List<string>();

    public KeyboardController()
    {
        _client = new KeyboardClient();
        _formatter = new KyeboardDataFormatter();
        _keyboardEmulator = new KeyboardLowLevelEmulator();

        _client.messageReceived += OnReceiveMessage;
    }

    ~KeyboardController() 
    {
        if (_client != null)
        {
            _client.messageReceived -= OnReceiveMessage;
        }
    }

    public void SendKey(string key, KeyboardKeyEvent keyboardEvent)
    {
        bool hasKey = Enum.TryParse(key, out KeyboardLowLevelEmulator.ScanCodeShort result);

        if (hasKey == false)
        {
            Console.WriteLine($"Keyboard error -> undefinded key: {key}; See list in {typeof(KeyboardLowLevelEmulator.ScanCodeShort)}");
            return;
        }

        switch (keyboardEvent)
        {
            case KeyboardKeyEvent.Hold:
                _keyboardEmulator.Send(result, KeyboardLowLevelEmulator.KEYEVENTF.SCANCODE);
                break;

            case KeyboardKeyEvent.Release:
                _keyboardEmulator.Send(result, KeyboardLowLevelEmulator.KEYEVENTF.KEYUP);
                break;

            case KeyboardKeyEvent.Tap:
                _keyboardEmulator.Send(result, KeyboardLowLevelEmulator.KEYEVENTF.SCANCODE);
                _keyboardEmulator.Send(result, KeyboardLowLevelEmulator.KEYEVENTF.KEYUP);
                break;

            default:
                throw new NotImplementedException($"Not defined realization for {nameof(keyboardEvent)}: {keyboardEvent}");
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