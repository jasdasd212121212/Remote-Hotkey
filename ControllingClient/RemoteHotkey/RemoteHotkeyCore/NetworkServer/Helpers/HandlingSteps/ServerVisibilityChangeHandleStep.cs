namespace RemoteHotkey.Network.Server;

public class ServerVisibilityChangeHandleStep : IPackegeHandlerStep<bool>
{
    public Action<bool> Callback { get; private set; }

    public ServerVisibilityChangeHandleStep(Action<bool> callback)
    {
        Callback = callback;
    }

    public void HandlePackege(byte[] data)
    {
        if (data[0] == 2)
        {
            Callback?.Invoke(data[1] > 0 ? true : false);
        }
    }
}