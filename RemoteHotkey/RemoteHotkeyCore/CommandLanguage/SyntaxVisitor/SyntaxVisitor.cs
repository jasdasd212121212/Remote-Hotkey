using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;

namespace RemoteHotkey.CommandLanguage.SyntaxVisitor;

public class SyntaxVisitor : ISyntaxVisitor
{
    private List<IToken> _tokens = new List<IToken>();
    private List<ICommandToken> _commands = new List<ICommandToken>();

    public IToken[] Tokens => _tokens.ToArray();
    public ICommandToken[] Commands => _commands.ToArray();

    public void Reset()
    {
        _tokens.Clear();
        _commands.Clear();
    }

    public void Visit(ICommandToken command)
    {
        _commands.Add(command);
        _tokens.Add(command);
    }

    public void Visit(LoopExpressionToken loop)
    {
        IToken[] tokens = loop.ExtractTokens();

        foreach (IToken token in tokens)
        {
            if (token is ICommandToken)
            {
                Visit(token as ICommandToken);
            }
            else
            {
                _tokens.Add(token);
            }
        }
    }
}