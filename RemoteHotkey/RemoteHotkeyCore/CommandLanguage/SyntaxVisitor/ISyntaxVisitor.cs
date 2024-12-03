using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

namespace RemoteHotkey.CommandLanguage.SyntaxVisitor;

public interface ISyntaxVisitor
{
    ICommandToken[] Tokens { get; }
    ICommandToken[] Commands { get; }

    void Reset();

    void Visit(ICommandToken command);
    void Visit(IExpressionToken loop);
}