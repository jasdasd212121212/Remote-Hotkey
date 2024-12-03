using RemoteHotkey.CommandLanguage.Helpers;
using RemoteHotkey.CommandLanguage;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

namespace RemoteHotkeyCore.CommandLanguage.Lexer.Base;

public abstract class LoopsLexingRoundBase<TLoopExpression> : ExpressionLexingRoundBase where TLoopExpression : IExpressionToken
{
    private TLoopExpression _loopExpression;

    private CommandLexingRound _commandLexer;
    private ArgumentExtractor _argumentExtractor;
    private ExpressionExtractor _expressionExtractor;

    public LoopsLexingRoundBase(IExpressionToken[] tokens, CommandLexingRound commandLexer) : base(null)
    {
        _commandLexer = commandLexer;
        _argumentExtractor = new ArgumentExtractor();
        _expressionExtractor = new ExpressionExtractor();

        _loopExpression = GetLoopExpression(null);
    }

    protected abstract TLoopExpression GetLoopExpression(IToken[] tokens);

    public override bool TryTokenize(string script, out string erasedScript, out IToken[] result)
    {
        List<IToken> resultList = new List<IToken>();

        if (Validate(script) == false)
        {
            result = null;
            erasedScript = script;

            return false;
        }

        string expression = _expressionExtractor.ExtractExpressions(script, out erasedScript);

        resultList.Add(TokenizeExpression(expression));

        result = resultList.ToArray();
        return true;
    }

    private bool Validate(string script)
    {
        if (script.Contains(_loopExpression.Name))
        {
            return true;
        }

        return false;
    }

    private TLoopExpression TokenizeExpression(string expression)
    {
        string[] separated = expression.Split(CommandLexerConstants.EXPRESSION_BODY_START_SYMBOL);

        string commands = separated[1];
        string expressionHead = separated[0];

        _commandLexer.TryTokenize(commands, out string erasedScript, out IToken[] result);

        TLoopExpression resultToken = GetLoopExpression(result);
        resultToken.Arguments = _argumentExtractor.GetArguments(expressionHead);

        return resultToken;
    }
}