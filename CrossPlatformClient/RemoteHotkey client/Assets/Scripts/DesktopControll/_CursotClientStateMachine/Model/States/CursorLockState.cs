using UnityEngine;

public class CursorLockState : State
{
    protected override void OnEnter()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}