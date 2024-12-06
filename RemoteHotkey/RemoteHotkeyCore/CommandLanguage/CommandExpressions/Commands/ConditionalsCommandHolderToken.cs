using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandSystem;
using RemoteHotkeyCore.CommandsSystem.Commands;

namespace RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;

public class ConditionalsCommandHolderToken : ICommandToken
{
    private Func<bool> _conditionals;
    private IToken _commandToken;

    public string Name => throw new NotImplementedException();

    public CommandArgumentToken[] Arguments { get; set; }

    public ConditionalsCommandHolderToken(IToken command, Func<bool> conditionals)
    {
        _commandToken = command;
        _conditionals = conditionals;
    }

    public void Accept(ISyntaxVisitor visitor) { }

    public IInputCommand ConstructCommand()
    {
        return new ConditionalsCommandCommandHolder(_commandToken, _conditionals);
    }

    public IToken CreateNew()
    {
        return new ConditionalsCommandHolderToken(_commandToken, _conditionals);
    }
}