using RemoteHotkey.InputsConstrollSystem;

namespace RemoteHotkey.CommandSystem;

public class CommandsPerformer
{
    private InputModel _inputMode;

    public CommandsPerformer(InputModel inputModel)
    {
        _inputMode = inputModel;
    }

    public async void Perform(CommandsAbstractFactory factory)
    {
        IInputCommand[] commands = factory.Create();

        foreach (IInputCommand command in commands)
        {
            command.Execute(_inputMode);
            await Task.Delay(150);
        }
    }
}