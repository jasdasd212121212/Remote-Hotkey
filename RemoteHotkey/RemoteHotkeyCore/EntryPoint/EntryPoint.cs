using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandSystem;
using RemoteHotkey.Network.Server;
using RemoteHotkey.ScreenCapture;
using RemoteHotkeyCore.EnterpreterEntry;
using RemoteHotkeyCore.ServerEntry;

namespace RemoteHotkeyCore;

public class EntryPoint
{
    public EntryPoint(IServer server) 
    {
        CommandsPerformer performer = new EnterpreterEntryPoint().Construct(out CommandLanguageLexer lexer);
        new ServerEntryPoint(server, performer, lexer);
        new ScreenCaptureNetworkSender(server);
    }
}