using UnityEngine;
using UnityEngine.UI;

public class ActivityControllPanelView : MonoBehaviour
{
    [SerializeField] private ClientPresenter _presenter;

    [Space]

    [SerializeField] private Button _setActiveButton;
    [SerializeField] private Button _setInactiveButton;

    private void Start()
    {
        _setActiveButton.onClick.AddListener(SetActive);
        _setInactiveButton.onClick.AddListener(SetInactive);
    }

    private void OnDestroy()
    {
        _setActiveButton.onClick.RemoveAllListeners();
        _setInactiveButton.onClick.RemoveAllListeners();
    }

    private void SetActive()
    {
        _presenter.SendServerWindowActivity(true);
    }

    private void SetInactive()
    {
        _presenter.SendServerWindowActivity(false);
    }
}