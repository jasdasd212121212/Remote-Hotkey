using Hooks;
using System.Windows.Forms;

namespace RemoteHotkeyCore.InputsController.Controllers;

public class KeyboardController
{
    private List<Keys> _keys = new List<Keys>();

    public KeyboardController()
    {
        KBDHook.KeyDown += new KBDHook.HookKeyPress(KeyPressed);
        KBDHook.KeyUp += new KBDHook.HookKeyPress(KeyReleased);
        KBDHook.LocalHook = false;
        KBDHook.InstallHook();
    }

    ~KeyboardController()
    {
        if (KBDHook.IsHookInstalled == true)
        {
            KBDHook.UnInstallHook();
        }
    }

    public Func<bool> ButtonIsPressed(Keys key) => () => _keys.Contains(key);   

    private void KeyPressed(LLKHEventArgs e)
    {
        if (_keys.Contains(e.Keys) == false)
        {
            _keys.Add(e.Keys);
        }
    }

    private void KeyReleased(LLKHEventArgs e)
    {
        _keys.Remove(e.Keys);
    }
}