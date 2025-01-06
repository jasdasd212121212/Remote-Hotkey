using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandSystem;
using RemoteHotkey.LowLevel;
using RemoteHotkey.Network.Observer;
using RemoteHotkey.Network.Server;

namespace RemoteHotkeyCore.ServerEntry;

public class ServerEntryPoint
{
    public ServerEntryPoint(IServer server, CommandsPerformer performer, CommandLanguageLexer lexer)
    {
        WindowVisibilityStateMachine visibilityStateMachine = new WindowVisibilityStateMachine();

        new ServerInternalCommandObserver(server, performer, new CommandsAbstractFactory[]
        {
            new TestCommandsFactory(),
            new LockCommandFactory(5)
        });

        new ServerScriptObserver(server, performer, new CommandLanguageEnterpreterFactory(lexer));
        new ServerWindowVisibilityChangingObserver(visibilityStateMachine, server);
    }
}