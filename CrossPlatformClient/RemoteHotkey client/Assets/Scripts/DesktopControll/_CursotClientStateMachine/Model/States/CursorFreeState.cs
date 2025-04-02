using UnityEngine;

public class CursorFreeState : State
{
    protected override void OnEnter()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}