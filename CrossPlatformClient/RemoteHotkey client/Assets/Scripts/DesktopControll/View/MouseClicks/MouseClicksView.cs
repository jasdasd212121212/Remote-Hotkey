using System;
using UnityEngine;

public class MouseClicksView : DesktopControllViewBase
{
    public event Action<CallbackMouseButton> mouseClicked;

    public MouseClicksView(ImageInputHelper image) : base(image)
    {
        DisplayImage.pointerClick += OnClick;
    }

    ~MouseClicksView() 
    {
        if (DisplayImage != null)
        {
            DisplayImage.pointerClick -= OnClick;
        }
    }

    private void OnClick()
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

        mouseClicked?.Invoke((CallbackMouseButton)currnetMouseButton);
    }
}