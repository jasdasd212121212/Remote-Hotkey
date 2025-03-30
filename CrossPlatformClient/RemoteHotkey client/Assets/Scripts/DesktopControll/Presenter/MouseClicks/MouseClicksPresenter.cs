using UnityEngine;

public class MouseClicksPresenter : DesktopControllPresenterBase<MouseClicksView>
{
    private const string UP_KEY_ACTION_ARGUMENT = "R";
    private const string DOWN_KEY_ACTION_ARGUMENT = "H";

    public MouseClicksPresenter(MouseClicksView view, DesktopControllModel model) : base(view, model)
    {
        View.mouseDown += OnDown;
        View.mouseUp += OnUp;
    }

    ~MouseClicksPresenter() 
    {
        if (View != null)
        {
            View.mouseDown -= OnDown;
            View.mouseUp -= OnUp;
        }
    }

    private void OnDown(CallbackMouseButton mouseButton)
    {
        ExecuteCommand(mouseButton, true);
    }

    private void OnUp(CallbackMouseButton mouseButton)
    {
        ExecuteCommand(mouseButton, false);
    }

    private void ExecuteCommand(CallbackMouseButton mouseButton, bool isDown)
    {
        Model.ExecuteCommand<ClickMouseButtonCommand>(GetArgumentMouse(mouseButton), isDown ? DOWN_KEY_ACTION_ARGUMENT : UP_KEY_ACTION_ARGUMENT);
    }

    private string GetArgumentMouse(CallbackMouseButton sourceButton)
    {
        switch (sourceButton)
        {
            case CallbackMouseButton.Left:
                return "L";
            case CallbackMouseButton.Right:
                return "R";
            case CallbackMouseButton.Middle:
                return "M";
            default:
                Debug.LogError($"Undefined mouse button: {sourceButton}");
                return "X";
        }
    }
}