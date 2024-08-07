using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClientConnectionPanelView : MonoBehaviour
{
    [SerializeField] private ClientConnectionModel _connectionModel;

    [Space]

    [SerializeField] private GameObject _panelGameObject;
    [SerializeField] private TMP_InputField _ipInputField;
    [SerializeField] private TMP_InputField _userNameField;
    [SerializeField] private Button _confirmIpButton;

    private void Awake()
    {
        _connectionModel.ipSaveLoaded += OnIpLoaded;
        _connectionModel.userNameLoaded += OnUserNameLoaded;

        _confirmIpButton.onClick.AddListener(Connect);
    }

    private void OnDestroy()
    {
        _connectionModel.ipSaveLoaded -= OnIpLoaded;
        _connectionModel.userNameLoaded -= OnUserNameLoaded;

        _confirmIpButton.onClick.RemoveAllListeners();
    }

    private void OnIpLoaded(string ip)
    {
        _ipInputField.text = ip;
    }

    private void OnUserNameLoaded(string userName)
    {
        _userNameField.text = userName;
    }

    private void Connect()
    {
        _panelGameObject.SetActive(false);
        _connectionModel.Connect(_ipInputField.text, _userNameField.text);
    }
}