using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

namespace RemoteHotkey.CommandLanguage.SyntaxVisitor;

public class SyntaxVisitor : ISyntaxVisitor
{
    private List<ICommandToken> _tokens = new List<ICommandToken>();
    private List<ICommandToken> _commands = new List<ICommandToken>();

    public ICommandToken[] Tokens => _tokens.ToArray();
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

    public void Visit(IExpressionToken expression)
    {
        ICommandToken[] tokens = expression.ExtractTokens().Select(token => token as ICommandToken).ToArray();

        foreach (ICommandToken token in tokens)
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