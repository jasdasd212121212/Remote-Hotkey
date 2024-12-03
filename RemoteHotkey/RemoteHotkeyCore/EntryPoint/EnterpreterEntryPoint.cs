using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

namespace RemoteHotkeyCore.EnterpreterEntry;

public class EnterpreterEntryPoint
{
    public CommandsPerformer Construct(out CommandLanguageLexer commandLexer)
    {
        CommandsPerformer performer = new CommandsPerformer(new InputModel());

        CommandLanguageLexer lexer = new CommandLanguageLexer(
            new ICommandToken[]
            {
                new LockMouseCommandToken(),
                new MouseClickCommandToken(),
                new MouseMoveCommandToken(),
                new UnlockMouseCommandToken(),
                new ExecuteBatchCommandToken(),
                new DebugCommandToken()
            },

            new IExpressionToken[]
            {
                new LoopExpressionToken(null)
            }
        );

        commandLexer = lexer;
        return performer;
    }
}