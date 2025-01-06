using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkeyCore.InputsController.KeyboardEmulator;

namespace RemoteHotkeyCore.CommandsSystem.Commands;

public class KeyboardButtonCommand : IInputCommand
{
    private string _key;
    private string _action;

    public KeyboardButtonCommand(string key, string action)
    {
        _key = key;
        _action = action;
    }

    public void Execute(InputModel inputModel)
    {
        KeyboardKeyEvent action;

        switch (_action.Trim().ToUpper())
        {
            case "H":
                action = KeyboardKeyEvent.Hold;
                break;

            case "R":
                action = KeyboardKeyEvent.Release; 
                break;

            case "T":
                action = KeyboardKeyEvent.Tap;
                break;

            default:
                throw new NotImplementedException($"Not defined action: '{_action}'");
        }

        inputModel.KeyboardsController.SendKey(_key, action);   
    }
}