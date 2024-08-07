using System.Text;
using TMPro;
using UnityEngine;

public class DebugConsole
{
    private static TextMeshProUGUI _text;
    private static bool _constructed;
    private static string _errorText;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Construct()
    {
        if (_constructed == true)
        {
            return;
        }

        Application.logMessageReceived += OnLog;

        _constructed = true;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void PostConstruct()
    {
        try
        {
            _text = GameObject.FindObjectOfType<SignatureMarcup_DebugConsoleText>().GetComponent<TextMeshProUGUI>();
            _text.text = _errorText;
        }
        catch { }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= OnLog;
    }

    private static void OnLog(string condition, string stackTrace, LogType type)
    {
        _errorText = new StringBuilder(_errorText).
                AppendLine("").
                AppendLine(condition).
                AppendLine(type.ToString()).
                AppendLine(stackTrace).
                ToString();

        if (_text != null)
        {
            _text.text = _errorText;
        } 
    }
}