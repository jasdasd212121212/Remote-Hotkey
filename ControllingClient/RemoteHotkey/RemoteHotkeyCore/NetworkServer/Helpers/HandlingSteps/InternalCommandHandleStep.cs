namespace RemoteHotkey.Network.Server;

public class InternalCommandHandleStep : IPackegeHandlerStep<int>
{
    public Action<int> Callback { get; private set; }

    public InternalCommandHandleStep(Action<int> callback)
    {
        Callback = callback;
    }

    public void HandlePackege(byte[] data)
    {
        if (data[0] == 0)
        {
            Callback?.Invoke((int)data[1]);
        }
    }
}