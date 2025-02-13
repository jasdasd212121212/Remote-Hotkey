using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageInputHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler
{
    [SerializeField][Min(0.001f)] private float _pointerMoveDelay = 0.1f;

    private float _nextMoveCallbackTime;
    private bool _isHover;

    public bool IsHover 
    {
        get => _isHover;

        private set
        {
            _isHover = value;
            hoverChange?.Invoke();
        }
    }

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
        IsHover = true;
        pointerDown?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHover = true;
        pointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHover = false;
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
        IsHover = false;
        pointerUp?.Invoke();
    }
}