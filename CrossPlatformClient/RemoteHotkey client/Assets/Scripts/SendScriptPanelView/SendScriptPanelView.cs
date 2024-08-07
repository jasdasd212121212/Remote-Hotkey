using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SendScriptPanelView : MonoBehaviour
{
    [SerializeField] private ClientPresenter _presenter;
    [SerializeField] private ScriptsSaverPresenter _saverPresenter;

    [Space]

    [SerializeField] private Button _sendScriptButton;
    [SerializeField] private Button _sendAndSaveScriptButton;

    [SerializeField] private TMP_InputField _scriptInputField;

    private void Start()
    {
        _sendScriptButton.onClick.AddListener(SendScript);

        _sendAndSaveScriptButton.onClick.AddListener(SendScript);
        _sendAndSaveScriptButton.onClick.AddListener(SaveScript);
    }

    private void OnDestroy()
    {
        _sendScriptButton.onClick.RemoveAllListeners();
        _sendAndSaveScriptButton.onClick.RemoveAllListeners();
    }

    private void SendScript()
    {
        _presenter.SendCustomScript(_scriptInputField.text);
    }

    private void SaveScript()
    {
        _saverPresenter.Save(_scriptInputField.text);
    }
}