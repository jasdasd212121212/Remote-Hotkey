using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkeyCore.InputsController.Controllers;

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
            string actionArgument = value.Length > 1 ? value[1].Argument : "NaA";

            if (argument != "R" && argument != "L" && argument != "M")
            {
                throw new InvalidDataException($"Critical error -> invalid argument; Token: {this} can take only: 'R' - Right; 'L' - Left or 'M' - Middle mouse buttons indexes");
            }

            if (value.Length > 1)
            {
                if (actionArgument != "H" && actionArgument != "R" && actionArgument != "C")
                {
                    throw new InvalidDataException($"Critical errpr -> invalid argument; Token: {this} can take only: 'H' - Hold or 'R' - Release or 'C' - Click arguments as a mouse actions");
                }
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
        string[] indexedArguments = ["L", "R", "M"];
        string[] indexedActionArguments = ["H", "R", "C"];

        int enumIndexOfButton = ConvertStringArgumentToEnum(indexedArguments, 0, out bool hasButton);
        int enumIndexOfAction = ConvertStringArgumentToEnum(indexedActionArguments, 1, out bool hasAction);

        MouseButtonsEnum button = (MouseButtonsEnum)enumIndexOfButton;
        MouseActionEnum action = MouseActionEnum.Click;

        if (hasAction)
        {
            action = (MouseActionEnum)enumIndexOfAction;
        }

        return new MouseClickCommand(button, action);
    }

    public IToken CreateNew()
    {
        return new MouseClickCommandToken();
    }

    private int ConvertStringArgumentToEnum(string[] indexArgs, int arguemntIndex, out bool hasArgument)
    {
        if (arguemntIndex >= _arguments.Length)
        {
            hasArgument = false;
            return 0;
        }

        string currentArgument = _arguments[arguemntIndex].Argument;

        for (int i = 0; i < indexArgs.Length; i++)
        {
            if (currentArgument == indexArgs[i])
            {
                hasArgument = true;
                return i;
            }
        }

        throw new ArgumentException($"Invalid argument: {currentArgument}");
    }
}