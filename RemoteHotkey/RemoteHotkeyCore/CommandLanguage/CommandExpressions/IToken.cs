using RemoteHotkey.CommandLanguage.SyntaxVisitor;

namespace RemoteHotkey.CommandLanguage;

public interface IToken
{
    public string Name { get; }
    CommandArgumentToken[] Arguments { get; set; }

    IToken CreateNew();
    void Accept(ISyntaxVisitor visitor);
}