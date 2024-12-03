using RemoteHotkey.CommandLanguage;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;
using RemoteHotkeyCore.CommandLanguage.Lexer.Base;
using RemoteHotkeyCore.InputsController.Controllers;

namespace RemoteHotkeyCore.CommandLanguage.Lexer.Rounds;

public class LoopCheckKeyboardButtonLexingRound : LoopsLexingRoundBase<LoopCheckKeyboardButton>
{
    private KeyboardController _keyboardController;

    public LoopCheckKeyboardButtonLexingRound(IExpressionToken[] tokens, CommandLexingRound commandLexer, KeyboardController keyboardController) : base(tokens, commandLexer)
    {
        _keyboardController = keyboardController;
    }

    protected override LoopCheckKeyboardButton GetLoopExpression(IToken[] tokens)
    {
        return new LoopCheckKeyboardButton(tokens, _keyboardController);
    }
}