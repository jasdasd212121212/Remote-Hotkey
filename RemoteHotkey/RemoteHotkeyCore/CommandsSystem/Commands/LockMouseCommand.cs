using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkey.CommandSystem;

public class LockMouseCommand : IInputCommand
{
    private int _timeInSeconds;

    public LockMouseCommand(int timeSeconds)
    {
        _timeInSeconds = timeSeconds;
    }

    public void Execute(InputModel inputModel)
    {
        inputModel.MouseController.Lock(_timeInSeconds * 10);
    }
}