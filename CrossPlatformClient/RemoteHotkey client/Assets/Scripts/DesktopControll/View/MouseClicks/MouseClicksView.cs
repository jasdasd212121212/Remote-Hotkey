using System;
using UnityEngine;

public class MouseClicksView : DesktopControllViewBase
{
    public event Action<CallbackMouseButton> mouseDown;
    public event Action<CallbackMouseButton> mouseUp;

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

        for (int i = 0; i <= 2; i++)
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

        for (int i = 0; i <= 2; i++)
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