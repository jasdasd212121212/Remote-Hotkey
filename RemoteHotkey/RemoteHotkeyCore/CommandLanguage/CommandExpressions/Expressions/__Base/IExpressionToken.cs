using RemoteHotkey.CommandLanguage;

namespace RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

public interface IExpressionToken : IToken
{
    public IToken[] ExtractTokens();
}