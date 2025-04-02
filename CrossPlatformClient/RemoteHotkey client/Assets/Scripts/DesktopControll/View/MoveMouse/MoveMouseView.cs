using System;
using UnityEngine;

public class MoveMouseView : DesktopControllViewBase
{
    public event Action<Vector2> mouseMoved;

    public MoveMouseView(ImageInputHelper image) : base(image)
    {
        DisplayImage.pointerMove += OnMouseMove;
    }

    ~MoveMouseView()
    {
        if (DisplayImage != null)
        {
            DisplayImage.pointerMove -= OnMouseMove;
        }
    }

    private void OnMouseMove()
    {
        mouseMoved?.Invoke(new Vector2(Input.GetAxis(MouseAxes.MOUSE_X), -Input.GetAxis(MouseAxes.MOUSE_Y)));
    }
}