namespace RemoteHotkey.InputsConstrollSystem;

public class InputModel
{
    public MouseController MouseController { get; private set; }

    public InputModel()
    {
        MouseController = new MouseController();
    }
}