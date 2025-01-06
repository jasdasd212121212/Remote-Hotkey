namespace RemoteHotkey.Network.Server;

public class ServerPackegeHandler
{
    private IPackegeHandlerStep[] _steps;

    public ServerPackegeHandler(Action<int> commandPerformReceived, Action<string> scriptExecutionReuqired, Action<bool> serverVisibilityChangeRequired)
    {
        _steps = new IPackegeHandlerStep[] 
        {
            new InternalCommandHandleStep(commandPerformReceived),
            new ExecuteScriptHandleStep(scriptExecutionReuqired),
            new ServerVisibilityChangeHandleStep(serverVisibilityChangeRequired)
        };
    }

    public void HandlePackege(byte[] packege)
    {
        foreach (IPackegeHandlerStep step in _steps)
        {
            step.HandlePackege(packege);
        }
    }
}