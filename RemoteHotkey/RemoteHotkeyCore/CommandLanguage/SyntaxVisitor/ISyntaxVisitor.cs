namespace RemoteHotkey.CommandLanguage.SyntaxVisitor;

public interface ISyntaxVisitor
{
    IToken[] Tokens { get; }
    ICommandToken[] Commands { get; }

    void Reset();

    void Visit(ICommandToken command);
    void Visit(LoopExpressionToken loop);
}