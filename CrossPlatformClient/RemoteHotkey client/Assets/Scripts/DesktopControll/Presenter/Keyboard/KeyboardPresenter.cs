using UnityEngine;

public class KeyboardPresenter : DesktopControllPresenterBase<KeyboardView>
{
    private DesktopControllModel _model;
    private KeyboardView _view;

    public KeyboardPresenter(KeyboardView view, DesktopControllModel model) : base(view, model)
    {
        _view = view;
        _model = model;

        _view.keyDown += OnKeyDown;
        _view.keyUp += OnKeyUp;
    }

    ~KeyboardPresenter()
    {
        if (_view != null)
        {
            _view.keyDown -= OnKeyDown;
            _view.keyUp -= OnKeyUp;
        }
    }

    private void OnKeyDown(string key)
    {
        Debug.Log(key);
    }

    private void OnKeyUp(string key) 
    {

    }
}