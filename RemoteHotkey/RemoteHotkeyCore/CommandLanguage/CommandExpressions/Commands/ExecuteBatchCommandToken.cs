using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandsSystem;
using RemoteHotkey.CommandSystem;

namespace RemoteHotkey.CommandLanguage;

public class ExecuteBatchCommandToken : ICommandToken
{
    public string Name => "CMD";

    public CommandArgumentToken[] Arguments { get; set; }

    public void Accept(ISyntaxVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IInputCommand ConstructCommand()
    {
        string[] commands = Arguments.Select(arg => arg.Argument).ToArray();

        return new ExecuteBatchCommand(commands);
    }

    public IToken CreateNew()
    {
        return new ExecuteBatchCommandToken();
    }
}