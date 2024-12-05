using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;
using RemoteHotkeyCore.InputsController.Controllers;
using System.Windows.Forms;

namespace RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;

public class LoopCheckKeyboardButton : IExpressionToken
{
    private KeyboardController _keyboardController;
    private CommandArgumentToken[] _arguments;
    private IToken[] _contaimendTokens;

    public string Name => "LP_Check_kb";

    public CommandArgumentToken[] Arguments
    {
        get => _arguments;

        set
        {
            _arguments = value;
        }
    }

    public LoopCheckKeyboardButton(IToken[] tokens, KeyboardController keyboardController)
    {
        _contaimendTokens = tokens;
        _keyboardController = keyboardController;
    }

    public void Accept(ISyntaxVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IToken CreateNew()
    {
        return new LoopCheckKeyboardButton(null, _keyboardController);
    }

    public IToken[] ExtractTokens()
    {
        List<IToken> result = new List<IToken>();
        int.TryParse(Arguments[1].Argument, out int iterations);

        for (int i = 0; i < iterations; i++)
        {
            result.AddRange(GetTokens());
        }

        return result.ToArray();
    }

    private ConditionalsCommandHolderToken[] GetTokens()
    {
        return _contaimendTokens.Select(token => new ConditionalsCommandHolderToken
        (
            token as ICommandToken,
            _keyboardController.ButtonIsPressed(Arguments[0].Argument)
        )).ToArray();
    }

    public IToken[] GetRawTokens()
    {
        return _contaimendTokens;
    }
}