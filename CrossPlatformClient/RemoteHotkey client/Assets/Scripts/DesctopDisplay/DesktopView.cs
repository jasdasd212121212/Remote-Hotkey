using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DesktopView : MonoBehaviour
{
    [SerializeField] private RawImage _target;

    [Inject] private DesktopModel _desktop;

    private void Awake()
    {
        _desktop.received += Display;
    }

    private void OnDestroy()
    {
        _desktop.received -= Display;
    }

    private void Display(Texture2D image)
    {
        _target.texture = image;
    }
}