using System.Numerics;
using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkey.CommandSystem;

public class TestCommandsFactory : CommandsAbstractFactory
{
    public override IInputCommand[] Create()
    {
        return new IInputCommand[]
        {
            new MouseMoveCommand(new Vector2(-2000, -2000)),
            new MouseMoveCommand(new Vector2(500, 250)),
            new MouseClickCommand(MouseButtonsEnum.Left)
        };
    }
}