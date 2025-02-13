using System;
using UnityEngine;

public class MoveMouseView : DesktopControllViewBase
{
    public event Action<Vector2> mouseMoved;

    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";

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
        mouseMoved?.Invoke(new Vector2(Input.GetAxis(MOUSE_X), -Input.GetAxis(MOUSE_Y)));
    }
}