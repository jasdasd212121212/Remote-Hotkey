using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandSystem;

namespace RemoteHotkey.CommandLanguage;

public class UnlockMouseCommandToken : ICommandToken
{
    private CommandArgumentToken[] _arguments;

    public string Name => "UNC_M";
    public CommandArgumentToken[] Arguments { get => _arguments; set => _arguments = value; }

    public IInputCommand ConstructCommand()
    {
        return new UnlockMouseCommand();
    }

    public void Accept(ISyntaxVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IToken CreateNew()
    {
        return new UnlockMouseCommandToken();
    }
}