namespace RemoteHotkey.CommandLanguage;

public abstract class ExpressionLexingRoundBase : LexingRoundBase
{
    protected ExpressionLexingRoundBase(IExpressionToken[] tokens) : base(tokens)
    {
    }
}