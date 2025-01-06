using RemoteHotkeyCore;

namespace RemoteHotkey.Network.Server;

public class RestartMessageHandler : IPackegeHandlerStep
{
    public void HandlePackege(byte[] data)
    {
        if (data[0] == 3)
        {
            GlobalEntryPoint.Restart();
        }
    }
}