using Hooks;

namespace RemoteHotkeyCore.InputsController.Controllers;

public class KeyboardController
{
    private HooksThread _hooks;

    private List<string> _keys = new List<string>();
    private List<int> _registred = new List<int>();

    private Dictionary<string, uint> _keyboard = new Dictionary<string, uint>()
    {
        { "backspace", 0x08 },
        { "tab", 0x09 },
        { "reverse", 0x0A0B },
        { "clear", 0x0C },
        { "enter", 0x0D },
        { "shift", 0x10 },
        { "ctrl", 0x11 },
        { "alt", 0x12 },
        { "pause", 0x13 },
        { "caps", 0x14 },
        { "escape", 0x1B },
        { "space", 0x20 },

        { "0", 0x30 },
        { "1", 0x31 },
        { "2", 0x32 },
        { "3", 0x33 },
        { "4", 0x34 },
        { "5", 0x35 },
        { "6", 0x36 },
        { "7", 0x37 },
        { "8", 0x38 },
        { "9", 0x39 },


        { "a", 0x41 },
        { "b", 0x42 },
        { "c", 0x43 },
        { "d", 0x44 },
        { "e", 0x45 },
        { "f", 0x46 },
        { "g", 0x47 },
        { "h", 0x48 },
        { "i", 0x49 },
        { "j", 0x4A },
        { "k", 0x4B },
        { "l", 0x4C },
        { "m", 0x4D },
        { "n", 0x4E },
        { "o", 0x4F },
        { "p", 0x50 },
        { "q", 0x51 },
        { "r", 0x52 },
        { "s", 0x53 },
        { "t", 0x54 },
        { "u", 0x55 },
        { "v", 0x56 },
        { "w", 0x57 },
        { "x", 0x58 },
        { "y", 0x59 },
        { "z", 0x5A },
    };

    public KeyboardController()
    {
        _hooks = new HooksThread();
        _hooks.Running = true;

        foreach (KeyValuePair<string, uint> current in _keyboard)
        {
            _registred.Add(_hooks.AddHotkey(current.Value, HotkeyModifiers.None, (msg) =>
            {
                string key = current.Key;
                OnPress(msg, key);
            }));
        }
    }

    ~KeyboardController()
    {
        _hooks.Running = false;
    }

    public Func<bool> ButtonIsPressed(string key) => () => _keys.Contains(key.ToLower());

    private void OnPress(Win32Msg message, string key)
    {
        _keys.Add(key);
    }
}