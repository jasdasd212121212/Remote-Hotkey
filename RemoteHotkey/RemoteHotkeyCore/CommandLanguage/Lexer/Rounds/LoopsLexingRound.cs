using RemoteHotkey.CommandLanguage.Helpers;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;
using RemoteHotkeyCore.CommandLanguage.Lexer.Base;

namespace RemoteHotkey.CommandLanguage;

public class LoopsLexingRound : LoopsLexingRoundBase<LoopExpressionToken>
{
    public LoopsLexingRound(IExpressionToken[] tokens, CommandLexingRound commandLexer) : base(tokens, commandLexer)
    {
    }

    protected override LoopExpressionToken GetLoopExpression(IToken[] tokens)
    {
        return new LoopExpressionToken(tokens);
    }

    //private CommandLexingRound _commandLexer;
    //private ArgumentExtractor _argumentExtractor;
    //private ExpressionExtractor _expressionExtractor;

    //public LoopsLexingRound(IExpressionToken[] tokens, CommandLexingRound commandLexer) : base(new IExpressionToken[] { new LoopCheckKeyboardButton(null) })
    //{
    //    _commandLexer = commandLexer;
    //    _argumentExtractor = new ArgumentExtractor();
    //    _expressionExtractor = new ExpressionExtractor();
    //}

    //public override bool TryTokenize(string script, out string erasedScript, out IToken[] result)
    //{
    //    List<IToken> resultList = new List<IToken>();

    //    if (Validate(script) == false)
    //    {
    //        result = null;
    //        erasedScript = script;

    //        return false;
    //    }

    //    string expression = _expressionExtractor.ExtractExpressions(script, out erasedScript);

    //    resultList.Add(TokenizeExpression(expression));

    //    result = resultList.ToArray();
    //    return true;
    //}

    //private bool Validate(string script)
    //{
    //    foreach (IToken token in Tokens)
    //    {
    //        if (script.Contains(token.Name))
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    //private LoopCheckKeyboardButton TokenizeExpression(string expression)
    //{
    //    string[] separated = expression.Split(CommandLexerConstants.EXPRESSION_BODY_START_SYMBOL);

    //    string commands = separated[1];
    //    string expressionHead = separated[0];

    //    _commandLexer.TryTokenize(commands, out string erasedScript, out IToken[] result);

    //    LoopCheckKeyboardButton resultToken = new LoopCheckKeyboardButton(result);
    //    resultToken.Arguments = _argumentExtractor.GetArguments(expressionHead);

    //    return resultToken;
    //}


}