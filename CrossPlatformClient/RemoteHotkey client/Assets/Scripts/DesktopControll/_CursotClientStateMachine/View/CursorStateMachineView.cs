using UnityEngine;
using Zenject;

public class CursorStateMachineView : MonoBehaviour
{
    [SerializeField] private CursorStateMachineViewSettings _settings;
    [SerializeField] private ImageInputHelper _imageInputHelper;

    [Inject] private CursorStateMachinePresenter _presenter;

    private void Awake()
    {
        _imageInputHelper.pointerClick += OnClick;
    }

    private void OnDestroy()
    {
        _imageInputHelper.pointerClick -= OnClick;
    }

    private void OnClick()
    {
        _presenter.Lock();
    }

    private void Update()
    {
        foreach (KeyCode key in _settings.ExitKeyCodeCombination)
        {
            if (Input.GetKey(key) == false)
            {
                return;
            }
        }

        _presenter.Unlock();
    }
}