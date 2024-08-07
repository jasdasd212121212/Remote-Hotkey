using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkey.CommandSystem;

public class MouseClickCommand : IInputCommand
{
    private MouseButtonsEnum _button;

    public MouseClickCommand(MouseButtonsEnum button)
    {
        _button = button;
    }

    public void Execute(InputModel inputModel)
    {
        inputModel.MouseController.Click(_button);
    }
}