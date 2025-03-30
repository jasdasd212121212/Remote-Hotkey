using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkey.CommandSystem;
using RemoteHotkeyCore.CommandsSystem.Commands;

namespace RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;

public class ScrollMouseWheelCommandToken : ICommandToken
{
    public string Name => "RT_W";

    private CommandArgumentToken[] _arguments;

    public CommandArgumentToken[] Arguments 
    { 
        get => _arguments;
        
        set
        {
            string argument = value[0].Argument;

            if (int.TryParse(argument, out int r) == false)
            {
                throw new ArgumentException($"Critical error -> argument of token: {this} can`t be parsed to Int16, argument: {argument}");
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
        return new ScrollMouseWheelComand(int.Parse(_arguments[0].Argument));
    }

    public IToken CreateNew()
    {
        return new ScrollMouseWheelCommandToken();
    }
}