using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageInputHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField][Min(0.001f)] private float _pointerMoveDelay = 0.1f;

    private float _nextMoveCallbackTime;

    public event Action pointerEnter;
    public event Action pointerExit;
    public event Action pointerUp;
    public event Action pointerDown;
    public event Action pointerMove;
    public event Action pointerClick;
    public event Action disabled;

    public event Action hoverChange;

    private void OnDisable()
    {
        disabled?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        pointerClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerExit?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            pointerDown?.Invoke();
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            pointerUp?.Invoke();
        }

        if (Time.time > _nextMoveCallbackTime)
        {
            if (Mathf.Abs(Input.GetAxis(MouseAxes.MOUSE_X)) > 0 || Mathf.Abs(Input.GetAxis(MouseAxes.MOUSE_Y)) > 0)
            {
                pointerMove?.Invoke();
            }

            _nextMoveCallbackTime = Time.time + _pointerMoveDelay;
        }
    }
}