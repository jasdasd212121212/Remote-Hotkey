using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

namespace RemoteHotkey.CommandLanguage;

public class ExpressionsPrepareRound : ExpressionLexingRoundBase
{
    public ExpressionsPrepareRound(IExpressionToken[] tokens) : base(tokens) {}

    public override bool TryTokenize(string script, out string erasedScript, out IToken[] result)
    {
        string editedScript = "";

        foreach (IToken token in Tokens)
        {
            editedScript = script.Replace(token.Name, $"{CommandLexerConstants.EXPRESSION_START_SYMBOL}{token.Name}");
        }

        result = null;
        erasedScript = editedScript;

        return false;
    }
}