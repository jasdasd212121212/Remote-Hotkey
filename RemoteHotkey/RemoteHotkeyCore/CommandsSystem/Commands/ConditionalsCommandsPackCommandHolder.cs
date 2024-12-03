using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkeyCore.CommandsSystem.Commands;

public class ConditionalsCommandsPackCommandHolder : IInputCommand
{
    private IInputCommand[] _commands;

    private Func<bool> condition;

    public ConditionalsCommandsPackCommandHolder(IInputCommand[] commands, Func<bool> condition)
    {
        _commands = commands;
        this.condition = condition;
    }

    public void Execute(InputModel inputModel)
    {
        if (condition() == true)
        {
            foreach (IInputCommand command in _commands)
            {
                command.Execute(inputModel);
            }
        }
    }
}