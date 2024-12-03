namespace RemoteHotkey.CommandLanguage;

public abstract class LexingRoundBase
{
    protected IToken[] Tokens;

    public LexingRoundBase(IToken[] tokens)
    {
        Tokens = tokens;
    }

    public abstract bool TryTokenize(string script, out string erasedScript, out IToken[] result);
}