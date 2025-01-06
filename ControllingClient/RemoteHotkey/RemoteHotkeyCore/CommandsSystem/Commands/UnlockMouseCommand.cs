using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkey.CommandSystem;

public class UnlockMouseCommand : IInputCommand
{
    public void Execute(InputModel inputModel)
    {
        inputModel.MouseController.Unlock();
    }
}