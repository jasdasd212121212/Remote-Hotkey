using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MouseClicksView : DesktopControllViewBase
{
    private List<int> _holdedButtons = new List<int>();
    private CancellationTokenSource _cancellationToken;

    private bool _isRunned;

    public event Action<CallbackMouseButton> mouseDown;
    public event Action<CallbackMouseButton> mouseUp;

    private const int MOUSE_BUTTONS = 2;

    public MouseClicksView(ImageInputHelper image) : base(image)
    {
        _cancellationToken = new CancellationTokenSource();
        _isRunned = true;

        DisplayImage.pointerDown += OnDown;
        DisplayImage.pointerUp += OnUp;
        DisplayImage.disabled += OnUp;

        RepeateLoop().Forget();
    }

    ~MouseClicksView() 
    {
        _isRunned = false;
        _cancellationToken?.Cancel();

        if (DisplayImage != null)
        {
            DisplayImage.pointerClick -= OnDown;
            DisplayImage.pointerUp -= OnUp;
            DisplayImage.disabled -= OnUp;
        }
    }

    private void OnDown()
    {
        int currnetMouseButton = 0;

        for (int i = 0; i <= MOUSE_BUTTONS; i++)
        {
            if (Input.GetMouseButtonDown(i))
            {
                if (!_holdedButtons.Contains(i))
                {
                    _holdedButtons.Add(i);
                }
                
                currnetMouseButton = i;
                break;
            }
        }

        mouseDown?.Invoke((CallbackMouseButton)currnetMouseButton);
    }

    private void OnUp()
    {
        int currnetMouseButton = 0;

        for (int i = 0; i <= MOUSE_BUTTONS; i++)
        {
            if (Input.GetMouseButtonUp(i))
            {
                if (_holdedButtons.Contains(i))
                {
                    _holdedButtons.Remove(i);
                }

                currnetMouseButton = i;
                break;
            }
        }

        mouseUp?.Invoke((CallbackMouseButton)currnetMouseButton);
    }

    private async UniTask RepeateLoop()
    {
        while (_isRunned)
        {
            for (int i = 0; i <= MOUSE_BUTTONS; i++)
            {
                if (!_holdedButtons.Contains(i))
                {
                    mouseUp?.Invoke((CallbackMouseButton)i);
                }
            }

            await UniTask.WaitForSeconds(3, cancellationToken: _cancellationToken.Token);
        }
    }
}