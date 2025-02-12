using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkey.Network.Server;
using RemoteHotkey.ScreenCapture;
using RemoteHotkeyCore.EnterpreterEntry;
using RemoteHotkeyCore.ServerEntry;

namespace RemoteHotkeyCore;

public class EntryPoint
{
    public EntryPoint(IServer server) 
    {
        InputModel inputModel = new InputModel();

        CommandsPerformer performer = new EnterpreterEntryPoint().Construct(out CommandLanguageLexer lexer, inputModel);
        new ServerEntryPoint(server, performer, lexer);
        new ScreenCaptureNetworkSender(server, inputModel.MouseController);
    }
}