using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandSystem;

namespace RemoteHotkey.CommandLanguage;

public class LockMouseCommandToken : ICommandToken
{
    private CommandArgumentToken[] _arguments;

    public string Name => "LC_M";
    public CommandArgumentToken[] Arguments
    {
        get => _arguments;

        set
        {
            if (value.Length > 1)
            {
                throw new Exception($"Critical error -> can`t parse command tokens because this token supports only 1 argument; Name: {Name}; Token: {this}; Argument: {value[0]}; Argument lenght: {value.Length}");
            }

            if (int.TryParse(value[0].Argument, out int num) == false)
            {
                throw new InvalidOperationException();
            }

            _arguments = value;
        }
    }

    public void Accept(ISyntaxVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IInputCommand ConstructCommand()
    {
        return new LockMouseCommand(int.Parse(Arguments[0].Argument));
    }

    public IToken CreateNew()
    {
        return new LockMouseCommandToken();
    }
}