namespace RemoteHotkey.Network.Observer;

using RemoteHotkey.Network.Server;
using RemoteHotkey.CommandSystem;

public class ServerInternalCommandObserver
{
    private IServer _server;

    private CommandsPerformer _performer;
    private CommandsAbstractFactory[] _factoryes;

    public ServerInternalCommandObserver(IServer observableServer, CommandsPerformer performer, CommandsAbstractFactory[] commandsFactoryes)
    {
        _server = observableServer;
        _performer = performer;
        _factoryes = commandsFactoryes;

        _server.commandPerformReceived += OnCommand;
    }

    ~ServerInternalCommandObserver()
    {
        try
        {
            _server.commandPerformReceived -= OnCommand;
        }
        catch { }
    }

    private void OnCommand(int id)
    {
        Console.WriteLine($"Performed commands squence: {_factoryes[id]}");
        _performer.Perform(_factoryes[id]);
    }
}