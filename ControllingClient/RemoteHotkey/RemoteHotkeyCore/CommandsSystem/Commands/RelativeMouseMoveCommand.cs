using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;
using System.Numerics;

namespace RemoteHotkeyCore.CommandsSystem.Commands;

public class RelativeMouseMoveCommand : IInputCommand
{
    private Vector2 _offset;

    public RelativeMouseMoveCommand(Vector2 offset)
    {
        _offset = offset;
    }

    public void Execute(InputModel inputModel)
    {
        inputModel.MouseController.SetMousePosition(_offset, false);   
    }
}