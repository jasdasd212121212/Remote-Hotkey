namespace RemoteHotkey.Network.Server;

public interface IServer
{
    bool IsConnected { get; }

    event Action<int> commandPerformReceived;
    event Action<string> scriptExecutionReuqired;
    event Action<bool> serverVisibilityChangeRequired;

    void SendMessageToClient(byte[] message);
    void Stop();
}