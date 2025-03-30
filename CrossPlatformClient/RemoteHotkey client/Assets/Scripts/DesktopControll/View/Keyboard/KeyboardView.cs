using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class KeyboardView : DesktopControllViewBase
{
    private CancellationTokenSource _cancellation;

    private Array _keys;

    private bool _isRunned;

    public event Action<string> keyDown;
    public event Action<string> keyUp;

    public KeyboardView(ImageInputHelper image) : base(image)
    {
        _cancellation = new CancellationTokenSource();

        _keys = Enum.GetValues(typeof(KeyCode));
        _isRunned = true;

        CheckLoop().Forget();
    }

    ~KeyboardView() 
    {
        _isRunned = false;
        _cancellation?.Cancel();
    }

    private async UniTask CheckLoop()
    {
        while (_isRunned)
        {
            foreach (KeyCode key in _keys)
            {
                if (Input.GetKeyDown(key) && KeyIsNotMouse(key))
                {
                    keyDown?.Invoke(GetOverridedKey(key.ToString()));
                }
            }

            foreach (KeyCode key in _keys)
            {
                if (Input.GetKeyUp(key) && KeyIsNotMouse(key))
                {
                    keyUp?.Invoke(GetOverridedKey(key.ToString()));
                }
            }

            await UniTask.WaitForSeconds(Time.deltaTime, cancellationToken: _cancellation.Token);
        }
    }

    private string GetOverridedKey(string key)
    {
        return key;
    }

    private bool KeyIsNotMouse(KeyCode key)
    {
        return key != KeyCode.Mouse0 && key != KeyCode.Mouse1
            && key != KeyCode.Mouse2 && key != KeyCode.Mouse3
            && key != KeyCode.Mouse4 && key != KeyCode.Mouse5
            && key != KeyCode.Mouse6;
    }
}