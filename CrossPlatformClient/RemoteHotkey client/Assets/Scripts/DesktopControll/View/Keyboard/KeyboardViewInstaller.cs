using UnityEngine;
using Zenject;

public class KeyboardViewInstaller : DesktopControllViewInstallerBase<KeyboardView>
{
    [Inject] private CursorStateMachinePresenter _cursorPresenter;

    private KeyboardView _view;

    protected override KeyboardView GetInstance(ImageInputHelper desktopViewImage)
    {
        _view = new KeyboardView(desktopViewImage);
        return _view;
    }

    private void Start()
    {
        _view.SetCursorPresenter(_cursorPresenter);
    }
}