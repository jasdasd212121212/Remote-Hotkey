using RemoteHotkey.Network.Server;

namespace RemoteHotkey.Network.Server;

public class ServerTechnicalMessageHandler
{
    private IPackegeHandlerStep[] _steps;

    public ServerTechnicalMessageHandler()
    {
        _steps = new IPackegeHandlerStep[] { new RestartMessageHandler() };
    }

    public void HandlePackege(byte[] packege)
    {
        foreach (IPackegeHandlerStep step in _steps)
        {
            step.HandlePackege(packege);
        }
    }
}