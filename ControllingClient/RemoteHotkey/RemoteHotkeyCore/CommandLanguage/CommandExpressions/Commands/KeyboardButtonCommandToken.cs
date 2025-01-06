using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandSystem;
using RemoteHotkeyCore.CommandsSystem.Commands;

namespace RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;

public class KeyboardButtonCommandToken : ICommandToken
{
    public string Name => "CL_KB";

    public CommandArgumentToken[] Arguments { get; set; }

    public void Accept(ISyntaxVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IInputCommand ConstructCommand()
    {
        return new KeyboardButtonCommand(Arguments[0].Argument, Arguments[1].Argument);
    }

    public IToken CreateNew()
    {
        return new KeyboardButtonCommandToken();
    }
}