using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageInputHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler
{
    [SerializeField][Min(0.001f)] private float _pointerMoveDelay = 0.1f;

    private float _nextMoveCallbackTime;

    public event Action pointerEnter;
    public event Action pointerExit;
    public event Action pointerUp;
    public event Action pointerDown;
    public event Action pointerMove;
    public event Action pointerClick;

    public event Action hoverChange;

    public void OnPointerClick(PointerEventData eventData)
    {
        pointerClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerExit?.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (Time.time > _nextMoveCallbackTime)
        {
            pointerMove?.Invoke();
            _nextMoveCallbackTime = Time.time + _pointerMoveDelay;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerUp?.Invoke();
    }
}