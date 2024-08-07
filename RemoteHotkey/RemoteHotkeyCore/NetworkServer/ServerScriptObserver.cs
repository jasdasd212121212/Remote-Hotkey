namespace RemoteHotkey.Network.Observer;

using RemoteHotkey.Network.Server;
using RemoteHotkey.CommandSystem;

public class ServerScriptObserver
{
    private IServer _server;
    private CommandsPerformer _performer;
    private CommandLanguageEnterpreterFactory _languageEnterpreterFactory;

    public ServerScriptObserver(IServer server, CommandsPerformer performer, CommandLanguageEnterpreterFactory factory)
    {
        _server = server;
        _performer = performer;
        _languageEnterpreterFactory = factory;

        _server.scriptExecutionReuqired += OnScriptReceived;
    }

    ~ServerScriptObserver()
    {
        try
        {
            _server.scriptExecutionReuqired -= OnScriptReceived;
        }
        catch { }
    }

    private void OnScriptReceived(string script)
    {
        _languageEnterpreterFactory.Compile(script);
        _performer.Perform(_languageEnterpreterFactory);
    }
}