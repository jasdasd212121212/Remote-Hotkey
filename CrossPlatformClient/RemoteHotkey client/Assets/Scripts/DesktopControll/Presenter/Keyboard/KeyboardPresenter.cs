using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class KeyboardPresenter : DesktopControllPresenterBase<KeyboardView>
{
    private DesktopControllModel _model;
    private KeyboardView _view;
    private KeyboardKeysDictionary _keyboardKeysDictionary;

    private const string HOLD_KEY_ARGUMENT = "H";
    private const string RELEASE_KEY_ARGUMENT = "R";

    public KeyboardPresenter(KeyboardView view, DesktopControllModel model) : base(view, model)
    {
        _keyboardKeysDictionary = new();

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

    private void OnKeyDown(KeyCode key)
    {
        if (TryConvertKeyToWinApiCall(key, out string apiKey))
        {
            _model.ExecuteCommand<PressKeyKeyboardCommand>(apiKey, HOLD_KEY_ARGUMENT); 
        }
    }

    private void OnKeyUp(KeyCode key) 
    {
        if (TryConvertKeyToWinApiCall(key, out string apiKey))
        {
            _model.ExecuteCommand<PressKeyKeyboardCommand>(apiKey, RELEASE_KEY_ARGUMENT);
        }
    }

    private bool TryConvertKeyToWinApiCall(KeyCode key, out string result)
    {
        if (_keyboardKeysDictionary.Keys.ContainsKey(key) == false)
        {
            Debug.LogError($"Keyboard error -> key: {key} are not defined");
            result = "";
            return false;
        }

        result = _keyboardKeysDictionary.Keys[key].Trim();
        return true;
    }
}