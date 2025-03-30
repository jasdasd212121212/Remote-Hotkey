using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkeyCore.CommandsSystem.Commands;

public class ScrollMouseWheelComand : IInputCommand
{
    private int _amount;

    public ScrollMouseWheelComand(int amount)
    {
        _amount = amount;
    }

    public void Execute(InputModel inputModel)
    {
        inputModel.MouseController.ScrollMouseWheel(_amount);
    }
}