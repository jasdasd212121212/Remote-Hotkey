using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandLanguage.SyntaxVisitor;

namespace RemoteHotkey.CommandSystem;

public class CommandLanguageEnterpreterFactory : CommandsAbstractFactory
{
    private CommandLanguageLexer _lexer;
    private SyntaxVisitor _syntaxVisitor;

    private ICommandToken[] _commands;

    public CommandLanguageEnterpreterFactory(CommandLanguageLexer lexer)
    {
        _lexer = lexer;
        _syntaxVisitor = new SyntaxVisitor();
    }

    public void Compile(string script)
    {
        _syntaxVisitor.Reset();
        VisitAll(script);

        _commands = _syntaxVisitor.Commands;
    }

    public override IInputCommand[] Create()
    {
        IInputCommand[] commands = new IInputCommand[_commands.Length];

        for (int i = 0; i < commands.Length; i++)
        {
            commands[i] = _commands[i].ConstructCommand();
        }

        return commands;
    }

    private void VisitAll(string script)
    {
        IToken[] rawTokens = _lexer.Tokenize(script);

        foreach (IToken token in rawTokens)
        {
            token.Accept(_syntaxVisitor);
        }
    }
}