using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Commands;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

namespace RemoteHotkeyCore.EnterpreterEntry;

public class EnterpreterEntryPoint
{
    public CommandsPerformer Construct(out CommandLanguageLexer commandLexer, InputModel inputModel)
    {
        CommandsPerformer performer = new CommandsPerformer(inputModel);

        CommandLanguageLexer lexer = new CommandLanguageLexer(
            new ICommandToken[]
            {
                new LockMouseCommandToken(),
                new MouseClickCommandToken(),
                new MouseMoveCommandToken(),
                new UnlockMouseCommandToken(),
                new ExecuteBatchCommandToken(),
                new DebugCommandToken(),
                new RelativeMouseMoveCommandToken(),
                new KeyboardButtonCommandToken(),
                new ScrollMouseWheelCommandToken()
            },

            new IExpressionToken[]
            {
                new LoopExpressionToken(null),
                new LoopCheckKeyboardButton(null, inputModel.KeyboardsController)
            },

            inputModel.KeyboardsController
        );

        commandLexer = lexer;
        return performer;
    }
}