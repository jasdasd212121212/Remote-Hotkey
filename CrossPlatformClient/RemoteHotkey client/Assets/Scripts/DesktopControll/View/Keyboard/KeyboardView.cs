using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KeyboardView : DesktopControllViewBase
{
    private CursorStateMachinePresenter _presenter;
    private CancellationTokenSource _cancellation;

    private Array _keys;
    private List<KeyCode> _pressedKeys = new List<KeyCode>();

    private bool _isRunned;

    public event Action<KeyCode> keyDown;
    public event Action<KeyCode> keyUp;

    private int _pressedKeysCount;

    public KeyboardView(ImageInputHelper image) : base(image)
    {
        _cancellation = new CancellationTokenSource();

        _keys = Enum.GetValues(typeof(KeyCode));
        _isRunned = true;

        CheckLoop().Forget();
        RepeatLoop().Forget();
    }

    ~KeyboardView() 
    {
        _isRunned = false;
        _cancellation?.Cancel();
    }

    public void SetCursorPresenter(CursorStateMachinePresenter presenter)
    {
        _presenter = presenter;
    }

    private async UniTask CheckLoop()
    {
        while (_isRunned)
        {
            if (_presenter != null)
            {
                if (_pressedKeysCount == 0 && _presenter.IsLocked == false)
                {
                    await UniTask.WaitForSeconds(Time.deltaTime / 100, cancellationToken: _cancellation.Token);
                    continue;
                }

                foreach (KeyCode key in _keys)
                {
                    if (Input.GetKeyDown(key) && KeyIsNotMouse(key))
                    {
                        keyDown?.Invoke(key);
                        
                        if (!_pressedKeys.Contains(key))
                        {
                            _pressedKeys.Add(key);
                            _pressedKeysCount++;
                        }
                    }
                }

                foreach (KeyCode key in _keys)
                {
                    if (Input.GetKeyUp(key) && KeyIsNotMouse(key))
                    {
                        keyUp?.Invoke(key);
                        _pressedKeysCount--;

                        _pressedKeys.Remove(key);
                    }
                }
            }

            await UniTask.WaitForSeconds(Time.deltaTime / 100, cancellationToken: _cancellation.Token);
        }
    }

    private async UniTask RepeatLoop()
    {
        while (_isRunned)
        {
            if (_presenter != null)
            {
                foreach (KeyCode key in _keys)
                {
                    if (!_pressedKeys.Contains(key))
                    {
                        keyUp.Invoke(key);
                    }
                }
            }

            await UniTask.WaitForSeconds(3, cancellationToken: _cancellation.Token);
        }
    }

    private bool KeyIsNotMouse(KeyCode key)
    {
        return key != KeyCode.Mouse0 && key != KeyCode.Mouse1
            && key != KeyCode.Mouse2 && key != KeyCode.Mouse3
            && key != KeyCode.Mouse4 && key != KeyCode.Mouse5
            && key != KeyCode.Mouse6;
    }
}