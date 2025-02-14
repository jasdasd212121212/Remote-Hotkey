using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkeyCore.InputsController.Controllers;

namespace RemoteHotkey.CommandSystem;

public class MouseClickCommand : IInputCommand
{
    private MouseButtonsEnum _button;
    private MouseActionEnum _action;

    public MouseClickCommand(MouseButtonsEnum button, MouseActionEnum action)
    {
        _button = button;
        _action = action;
    }

    public void Execute(InputModel inputModel)
    {
        inputModel.MouseController.Click(_button, _action);
    }
}