using UnityEngine;
using TMPro;
using Zenject;

public class ScriptsSaverFieldSetter : MonoBehaviour
{
    [SerializeField] private TMP_InputField _scriptInputField;

    [Inject] private ScriptsSaverModel _saver;

    private void Awake()
    {
        _saver.scriptLoaded += OnScriptLoaded;
    }

    private void OnDestroy()
    {
        _saver.scriptLoaded -= OnScriptLoaded;
    }

    private void OnScriptLoaded(string script)
    {
        _scriptInputField.text = script;
    }
}