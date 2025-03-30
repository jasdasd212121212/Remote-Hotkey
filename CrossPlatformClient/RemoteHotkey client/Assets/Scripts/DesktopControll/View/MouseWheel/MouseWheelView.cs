using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class MouseWheelView : DesktopControllViewBase
{
    private CancellationTokenSource _cancellation;

    private float _updateDelay;
    private float _sendDelay;

    private float _nextSendTime;

    public event Action<float> wheelRotated;

    public MouseWheelView(ImageInputHelper image, float updateDelay, float sendDelay) : base(image)
    {
        _sendDelay = sendDelay;
        _updateDelay = updateDelay;
        _cancellation = new CancellationTokenSource();
        
        UpdateLoop().Forget();
    }

    ~MouseWheelView() 
    {
        _cancellation.Cancel();
    }

    private async UniTask UpdateLoop()
    {
        while (true)
        {
            if (Input.mouseScrollDelta != Vector2.zero && Time.time > _nextSendTime)
            {
                wheelRotated?.Invoke(Input.mouseScrollDelta.y);
                _nextSendTime = Time.time + _sendDelay;
            }

            await UniTask.WaitForSeconds(_updateDelay, cancellationToken: _cancellation.Token);
        }
    }
}