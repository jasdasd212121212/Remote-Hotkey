using RemoteHotkey.CommandLanguage;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;
using RemoteHotkeyCore.CommandLanguage.Lexer.Base;
using RemoteHotkeyCore.InputsController.Controllers;

namespace RemoteHotkeyCore.CommandLanguage.Lexer.Rounds;

public class LoopCheckKeyboardButtonLexingRound : LoopsLexingRoundBase<LoopCheckKeyboardButton>
{
    private IExpressionToken[] _cachedTokens;
    private CommandLexingRound _commandLexer;
    private KeyboardController _keyboardController;
    private CommandLanguageLexer _mainLexer;

    public LoopCheckKeyboardButtonLexingRound(IExpressionToken[] tokens, CommandLexingRound commandLexer, KeyboardController keyboardController, CommandLanguageLexer mainLexer) : base(tokens, commandLexer, mainLexer)
    {
        _cachedTokens = tokens;
        _commandLexer = commandLexer;
        _keyboardController = keyboardController;
        _mainLexer = mainLexer;
    }

    protected override LexingRoundBase GetClone()
    {
        return new LoopCheckKeyboardButtonLexingRound(_cachedTokens, _commandLexer.CloneSelf() as CommandLexingRound, _keyboardController, _mainLexer);
    }

    protected override LoopCheckKeyboardButton GetLoopExpression(IToken[] tokens)
    {
        return new LoopCheckKeyboardButton(tokens, _keyboardController);
    }
}