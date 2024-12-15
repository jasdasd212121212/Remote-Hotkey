using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class ClientConnectionModel : MonoBehaviour
{
    [SerializeField] private bool _listening;

    [Inject] private IClient _client;

    public event Action<string> ipSaveLoaded;
    public event Action<string> userNameLoaded;

    private const string IP_SAVE_KEY = "lastIp";
    private const string USER_NAME_SAVE_KEY = "lastUser";

    private void Start()
    {
        if (PlayerPrefs.HasKey(IP_SAVE_KEY) == true)
        {
            ipSaveLoaded?.Invoke(PlayerPrefs.GetString(IP_SAVE_KEY));
            userNameLoaded?.Invoke(PlayerPrefs.GetString(USER_NAME_SAVE_KEY));
        }
        else
        {
            ipSaveLoaded?.Invoke("192.168.");
            PlayerPrefs.SetString(IP_SAVE_KEY, "192.168.");
        }
    }

    public void Connect(string ip, string userName)
    {
        _client.Connect(ip, userName).Forget();

        PlayerPrefs.SetString(IP_SAVE_KEY, ip);
        PlayerPrefs.SetString(USER_NAME_SAVE_KEY, userName);
    }
}