using UnityEngine;
using Zenject;

public class MouseWheelViewInstaller : DesktopControllViewInstallerBase<MouseWheelView>
{
    [SerializeField][Min(0.000001f)] private float _updateDelay;
    [SerializeField][Min(0.001f)] private float _sendDelay;

    [Inject] private CursorStateMachinePresenter _cursorPresenter;

    private MouseWheelView _view;

    protected override MouseWheelView GetInstance(ImageInputHelper desktopViewImage)
    {
        _view = new MouseWheelView(desktopViewImage, _updateDelay, _sendDelay);
        return _view;
    }

    private void Start()
    {
        _view.SetCursorPresenter(_cursorPresenter);
    }
}