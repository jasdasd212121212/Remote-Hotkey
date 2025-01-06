using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkey.CommandLanguage;

public class MouseClickCommandToken : ICommandToken
{
    private CommandArgumentToken[] _arguments;

    public string Name => "CL_M";
    public CommandArgumentToken[] Arguments
    {
        get => _arguments;

        set
        {
            string argument = value[0].Argument;

            if (argument != "R" && argument != "L" && argument != "M")
            {
                throw new InvalidDataException($"Critical error -> invalid argument; Token: {this} can take only: 'R' - Right; 'L' - Left or 'M' - Middle mouse buttons indexes");
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
        string argument = _arguments[0].Argument;
        string[] indexedArguments = new string[] { "L", "R", "M" };

        for (int i = 0; i < indexedArguments.Length; i++)
        {
            if (argument == indexedArguments[i])
            {
                return new MouseClickCommand((MouseButtonsEnum)i);
            }
        }

        throw new InvalidDataException();
    }

    public IToken CreateNew()
    {
        return new MouseClickCommandToken();
    }
}