using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MouseClicksView : DesktopControllViewBase
{
    public event Action<CallbackMouseButton> mouseDown;
    public event Action<CallbackMouseButton> mouseUp;

    private const int MOUSE_BUTTONS = 2;

    public MouseClicksView(ImageInputHelper image) : base(image)
    {
        DisplayImage.pointerDown += OnDown;
        DisplayImage.pointerUp += OnUp;
        DisplayImage.disabled += OnUp;
    }

    ~MouseClicksView() 
    {
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
                currnetMouseButton = i;
                break;
            }
        }

        mouseUp?.Invoke((CallbackMouseButton)currnetMouseButton);
    }
}