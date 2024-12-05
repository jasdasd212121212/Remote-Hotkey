using RemoteHotkeyCore.CommandLanguage.__Interfaces;

namespace RemoteHotkey.CommandLanguage;

public abstract class LexingRoundBase : IClonableRound
{
    protected IToken[] Tokens;

    public LexingRoundBase(IToken[] tokens)
    {
        Tokens = tokens;
    }

    public IClonableRound CloneSelf()
    {
        return GetClone();
    }

    public abstract bool TryTokenize(string script, out string erasedScript, out IToken[] result);
    protected abstract LexingRoundBase GetClone();
}