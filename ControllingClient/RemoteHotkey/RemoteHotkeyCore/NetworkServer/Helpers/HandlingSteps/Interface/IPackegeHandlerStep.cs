namespace RemoteHotkey.Network.Server;

public interface IPackegeHandlerStep<T> : IPackegeHandlerStep
{
    Action<T> Callback { get; }
}

public interface IPackegeHandlerStepWithCallback : IPackegeHandlerStep
{
    Action Callback { get; }
}

public interface IPackegeHandlerStep
{
    void HandlePackege(byte[] data);
}