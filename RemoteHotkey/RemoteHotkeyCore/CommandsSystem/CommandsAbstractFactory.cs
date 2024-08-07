namespace RemoteHotkey.CommandSystem;

public abstract class CommandsAbstractFactory
{
    public abstract IInputCommand[] Create();
}