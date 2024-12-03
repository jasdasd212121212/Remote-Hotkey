using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkeyCore.CommandsSystem.Commands;

public class ConditionalsCommandCommandHolder : IInputCommand
{
    private ICommandToken _command;

    private Func<bool> condition;

    public ConditionalsCommandCommandHolder(ICommandToken commands, Func<bool> condition)
    {
        _command = commands;
        this.condition = condition;
    }

    public void Execute(InputModel inputModel)
    {
        if (condition() == true)
        {
            _command.ConstructCommand().Execute(inputModel);
        }
    }
}