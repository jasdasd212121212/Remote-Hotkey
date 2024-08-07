namespace RemoteHotkey.CommandSystem;

public class LockCommandFactory : CommandsAbstractFactory
{
    private int _time;

    public LockCommandFactory(int time)
    {
        _time = time;
    }

    public override IInputCommand[] Create()
    {
        return new IInputCommand[] { new LockMouseCommand(_time) };
    }
}