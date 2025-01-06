using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

namespace RemoteHotkey.CommandLanguage;

public class ExpressionsPrepareRound : ExpressionLexingRoundBase
{
    private IExpressionToken[] _expressionTokens;

    public ExpressionsPrepareRound(IExpressionToken[] tokens) : base(tokens) 
    {
        _expressionTokens = tokens;
    }

    public override bool TryTokenize(string script, out string erasedScript, out IToken[] result)
    {
        string editedScript = script;

        foreach (IToken token in Tokens)
        {
            editedScript = editedScript.Replace(token.Name, $"{CommandLexerConstants.EXPRESSION_START_SYMBOL}{token.Name}");
        }

        result = null;
        erasedScript = editedScript;

        return false;
    }

    protected override LexingRoundBase GetClone()
    {
        return new ExpressionsPrepareRound(_expressionTokens);
    }
}