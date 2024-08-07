namespace RemoteHotkey.CommandLanguage;

public interface IExpressionToken : IToken
{
    public IToken[] ExtractTokens();
}