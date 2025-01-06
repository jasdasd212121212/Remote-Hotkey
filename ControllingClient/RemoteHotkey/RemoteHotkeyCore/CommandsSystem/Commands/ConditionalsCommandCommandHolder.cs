using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

namespace RemoteHotkeyCore.CommandsSystem.Commands;

public class ConditionalsCommandCommandHolder : IInputCommand
{
    private IToken _command;

    private Func<bool> condition;

    public ConditionalsCommandCommandHolder(IToken commands, Func<bool> condition)
    {
        _command = commands;
        this.condition = condition;
    }

    public void Execute(InputModel inputModel)
    {
        if (condition() == true && _command != null)
        {
            if (typeof(IExpressionToken).IsAssignableFrom(_command.GetType()))
            {
                IExpressionToken expression = (_command as IExpressionToken);
                ICommandToken[] commands = expression.ExtractTokens().Select(token => token as ICommandToken).ToArray();

                foreach (ICommandToken command in commands)
                {
                    command.ConstructCommand().Execute(inputModel);
                }
            }
            else
            {
                (_command as ICommandToken).ConstructCommand().Execute(inputModel);
            }
        }
    }
}