using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;
using RemoteHotkeyCore.CommandLanguage.Lexer.Base;

namespace RemoteHotkey.CommandLanguage;

public class LoopsLexingRound : LoopsLexingRoundBase<LoopExpressionToken>
{
    private IExpressionToken[] _cachedTokens;
    private CommandLexingRound _commandLexer;
    private CommandLanguageLexer _mainLexer;

    public LoopsLexingRound(IExpressionToken[] tokens, CommandLexingRound commandLexer, CommandLanguageLexer mainLexer) : base(tokens, commandLexer, mainLexer)
    {
        _cachedTokens = tokens;
        _commandLexer = commandLexer;
        _mainLexer = mainLexer;
    }

    protected override LexingRoundBase GetClone()
    {
        return new LoopsLexingRound(_cachedTokens, _commandLexer.CloneSelf() as CommandLexingRound, _mainLexer);
    }

    protected override LoopExpressionToken GetLoopExpression(IToken[] tokens)
    {
        return new LoopExpressionToken(tokens);
    }
}