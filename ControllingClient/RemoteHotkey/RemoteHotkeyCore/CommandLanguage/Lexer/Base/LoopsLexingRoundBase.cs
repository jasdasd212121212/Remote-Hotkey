using RemoteHotkey.CommandLanguage.Helpers;
using RemoteHotkey.CommandLanguage;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;
using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using System.Linq;

namespace RemoteHotkeyCore.CommandLanguage.Lexer.Base;

public abstract class LoopsLexingRoundBase<TLoopExpression> : ExpressionLexingRoundBase where TLoopExpression : IExpressionToken
{
    private TLoopExpression _loopExpression;

    private CommandLanguageLexer _mainLexer;
    private CommandLexingRound _commandLexer;
    private ArgumentExtractor _argumentExtractor;
    private ExpressionExtractor _expressionExtractor;

    public LoopsLexingRoundBase(IExpressionToken[] tokens, CommandLexingRound commandLexer, CommandLanguageLexer mainLexer) : base(null)
    {
        _mainLexer = mainLexer;
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

        string expression = _expressionExtractor.ExtractExpressions(script, out string erasedSimple, false);
        string expressionBody = _expressionExtractor.ExtractExpressionBody(expression, out string e);

        string all = _expressionExtractor.ExtractExpressions(script, out string erasedDefficult, true);
        int count = all.Where(currentSymbol => currentSymbol == CommandLexerConstants.EXPRESSION_START_SYMBOL).ToArray().Length - 1;
        string allBody = _expressionExtractor.ExtractExpressionBody(all, out string rr);
        allBody = allBody.Remove(allBody.Length - 2);

        TLoopExpression loop = default;

        if (expressionBody.Contains(CommandLexerConstants.EXPRESSION_START_SYMBOL) == false)
        {
            loop = TokenizeExpression(expression);

            erasedScript = erasedSimple;
        }
        else
        {
            loop = TokenizeDifficultExpression(allBody, all);

            erasedScript = erasedDefficult;
        }

        resultList.Add(loop);

        result = resultList.ToArray();
        return true;
    }

    private bool Validate(string script)
    {
        string header = script.Split(CommandLexerConstants.EXPRESSION_BODY_START_SYMBOL)[0];

        if (header.Contains(_loopExpression.Name))
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

    private TLoopExpression TokenizeDifficultExpression(string expression, string parentExpression)
    {
        CommandLanguageLexer lexer = _mainLexer.CloneSelf() as CommandLanguageLexer;

        string[] separated = parentExpression.Split(CommandLexerConstants.EXPRESSION_BODY_START_SYMBOL);

        string commands = separated[1];
        string expressionHead = separated[0];

        IToken[] result = lexer.Tokenize(expression.Replace(CommandLexerConstants.EXPRESSION_START_SYMBOL, ' '));

        TLoopExpression resultToken = GetLoopExpression(result);
        resultToken.Arguments = _argumentExtractor.GetArguments(expressionHead);

        return resultToken;
    }
}