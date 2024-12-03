using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandSystem;
using RemoteHotkeyCore.CommandsSystem.Commands;

namespace RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;

internal class DebugCommandToken : ICommandToken
{
    public string Name => "DBG";

    public CommandArgumentToken[] Arguments { get; set; }

    public void Accept(ISyntaxVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IInputCommand ConstructCommand()
    {
        return new DebugCommand(Arguments[0].Argument);
    }

    public IToken CreateNew()
    {
        return new DebugCommandToken();
    }
}