using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandSystem;
using RemoteHotkeyCore.CommandsSystem.Commands;
using System.Numerics;

namespace RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;

public class RelativeMouseMoveCommandToken : ICommandToken
{
    private CommandArgumentToken[] _arguments;

    public string Name => "MV_MR";

    public CommandArgumentToken[] Arguments
    {
        get => _arguments;

        set
        {
            if (value.Length < 2 || value.Length > 2)
            {
                throw new Exception($"Critical error -> can`t parse command tokens because this token supports only 2 argument; Name: {Name}; Token: {this}; Argument lenght: {value.Length}");
            }

            if (int.TryParse(value[0].Argument, out int number) == false ||
                int.TryParse(value[1].Argument, out int numberSecond) == false)
            {
                throw new InvalidOperationException($"Arguments: {value[0].Argument}, {value[1].Argument}");
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
        return new RelativeMouseMoveCommand(new Vector2(int.Parse(Arguments[0].Argument), int.Parse(Arguments[1].Argument)));
    }

    public IToken CreateNew()
    {
        return new RelativeMouseMoveCommandToken();
    }
}