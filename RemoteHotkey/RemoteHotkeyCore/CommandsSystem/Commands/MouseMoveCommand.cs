using RemoteHotkey.InputsConstrollSystem;
using System.Numerics;

namespace RemoteHotkey.CommandSystem;

public class MouseMoveCommand : IInputCommand
{
    private Vector2 _targetPosition;

    public MouseMoveCommand(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    public void Execute(InputModel inputModel)
    {
        inputModel.MouseController.SetMousePosition(_targetPosition, true);
    }
}