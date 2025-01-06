using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkeyCore.CommandsSystem.Commands;

public class DebugCommand : IInputCommand
{
    private string _text;

    public DebugCommand(string text)
    {
        _text = text;
    }

    public void Execute(InputModel inputModel)
    {
        Console.WriteLine(_text);
    }
}