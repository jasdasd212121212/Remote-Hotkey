using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkey.CommandSystem;

public interface IInputCommand
{
    void Execute(InputModel inputModel);
}