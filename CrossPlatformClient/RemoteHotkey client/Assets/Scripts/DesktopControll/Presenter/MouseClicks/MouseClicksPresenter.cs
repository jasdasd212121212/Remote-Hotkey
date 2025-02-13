using UnityEngine;

public class MouseClicksPresenter : DesktopControllPresenterBase<MouseClicksView>
{
    public MouseClicksPresenter(MouseClicksView view, DesktopControllModel model) : base(view, model)
    {
        View.mouseClicked += OnClick;
    }

    ~MouseClicksPresenter() 
    {
        if (View != null)
        {
            View.mouseClicked -= OnClick;
        }
    }

    private void OnClick(CallbackMouseButton mouseButton)
    {
        Model.ExecuteCommand<ClickMouseButtonCommand>(GetArgumentMouse(mouseButton));
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