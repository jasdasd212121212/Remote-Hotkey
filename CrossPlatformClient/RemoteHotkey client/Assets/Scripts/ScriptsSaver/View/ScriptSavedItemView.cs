using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScriptSavedItemView : MonoBehaviour
{
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _executeButton;
    [SerializeField] private Button _deleteButton;

    [Space()]

    [SerializeField] private TextMeshProUGUI _nameText;

    [Header("Renaming")]

    [SerializeField] private Button _renameButton;
    [SerializeField] private Button _confirmFileNameButton;
    [SerializeField] private TMP_InputField _newNameInputField;

    private ScriptsSaverPresenter _presenter;

    private string _scriptName;
    private bool _isInitialized;

    public void Initialize(string name, ScriptsSaverPresenter presenter)
    {
        if (_isInitialized == true)
        {
            return;
        }

        _presenter = presenter;
        _scriptName = name;

        _nameText.text = name;

        _newNameInputField.gameObject.SetActive(false);
        _confirmFileNameButton.gameObject.SetActive(false);

        SubscribeToAllEvents();

        _isInitialized = true;
    }

    private void SubscribeToAllEvents()
    {
        _loadButton.onClick.AddListener(LoadScript);
        _executeButton.onClick.AddListener(ExecuteScript);
        _deleteButton.onClick.AddListener(DeleteScript);
        _renameButton.onClick.AddListener(StartScriptRenaming);
        _confirmFileNameButton.onClick.AddListener(RenameScript);
    }

    private void OnDestroy()
    {
        _loadButton.onClick.RemoveAllListeners();
        _executeButton.onClick.RemoveAllListeners();
        _deleteButton.onClick.RemoveAllListeners();
        _renameButton.onClick.RemoveAllListeners();
        _confirmFileNameButton.onClick.RemoveAllListeners();
    }

    private void LoadScript()
    {
        _presenter.Load(_scriptName);
    }

    private void ExecuteScript()
    {
        _presenter.Execute(_scriptName);
    }

    private void DeleteScript()
    {
        _presenter.Delete(_scriptName);
    }

    private void StartScriptRenaming()
    {
        _newNameInputField.gameObject.SetActive(true);
        _renameButton.gameObject.SetActive(false);
        _confirmFileNameButton.gameObject.SetActive(true);
    }

    private void RenameScript()
    {
        _presenter.Rename(_scriptName, _newNameInputField.text);
    }
}