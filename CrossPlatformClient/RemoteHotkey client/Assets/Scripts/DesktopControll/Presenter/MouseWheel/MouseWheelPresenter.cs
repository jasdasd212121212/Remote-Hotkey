using UnityEngine;

public class MouseWheelPresenter : DesktopControllPresenterBase<MouseWheelView>
{
    public MouseWheelPresenter(MouseWheelView view, DesktopControllModel model) : base(view, model)
    {
        View.wheelRotated += OnRotate;
    }

    ~MouseWheelPresenter() 
    {
        View.wheelRotated -= OnRotate;
    }

    private void OnRotate(float delta)
    {
        Model.ExecuteCommand<RotateMouseWheelCommand>(Mathf.RoundToInt(delta * 100).ToString());
    }
}