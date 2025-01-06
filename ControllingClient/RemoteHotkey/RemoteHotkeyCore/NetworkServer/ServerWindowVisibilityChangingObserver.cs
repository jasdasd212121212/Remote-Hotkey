using RemoteHotkey.LowLevel;

namespace RemoteHotkey.Network.Observer;
using RemoteHotkey.Network.Server;

public class ServerWindowVisibilityChangingObserver
{
    private WindowVisibilityStateMachine _window;
    private IServer _server;

    public ServerWindowVisibilityChangingObserver(WindowVisibilityStateMachine window, IServer server)
    {
        _server = server;
        _window = window;

        _server.serverVisibilityChangeRequired += VisibilityChangingRequired;
    }

    ~ServerWindowVisibilityChangingObserver()
    {
        _server.serverVisibilityChangeRequired -= VisibilityChangingRequired;

        _window.SetVisibilityState(true);
    }

    private void VisibilityChangingRequired(bool state)
    {
        _window.SetVisibilityState(state);
    }
}