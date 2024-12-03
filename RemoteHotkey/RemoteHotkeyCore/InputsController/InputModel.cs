using RemoteHotkeyCore.InputsController.Controllers;

namespace RemoteHotkey.InputsConstrollSystem;

public class InputModel
{
    public MouseController MouseController { get; private set; }
    public KeyboardController KeyboardsController { get; private set; }

    public InputModel()
    {
        MouseController = new MouseController();
        KeyboardsController = new KeyboardController();
    }
}