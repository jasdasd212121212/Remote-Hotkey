using Zenject;
using UnityEngine;
using TMPro;

public class ClientStatusView : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] protected TextMeshProUGUI _statusText;

    [Inject] private RemoteHotkeyClientModel _clientModel;

    private void Awake()
    {
        _panel.SetActive(true);
        _statusText.text = "Connecting...";

        _clientModel.connected += OnConnected;
        _clientModel.disconnected += OnDisconnected;
        _clientModel.disconnectedWithError += OnError;
    }

    private void OnDestroy()
    {
        _clientModel.connected -= OnConnected;
        _clientModel.disconnected -= OnDisconnected;
        _clientModel.disconnectedWithError -= OnError;
    }

    private void OnConnected()
    {
        _panel.SetActive(false);
    }

    private void OnDisconnected()
    {
        try
        {
            _panel.SetActive(true);
            _statusText.text = "Disconnected";
        }
        catch { }
    }

    private void OnError(string error)
    {
        _panel.SetActive(true);
        _statusText.text = $"Connection error (maybe server not runned): {error}";
    }
}