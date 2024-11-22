using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class DesktopModel
{
    private RemoteHotkeyClientModel _client;
    private Texture2D _tempTexture;

    public event Action<Texture2D> received;

    public DesktopModel(RemoteHotkeyClientModel client)
    {
        _client = client;
        _tempTexture = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);

        _client.receivedClientDirectedMessage += Convert;
    }

    ~DesktopModel()
    {
        _client.receivedClientDirectedMessage -= Convert;
    }

    private void Convert(byte[] message)
    {
        ConverAsync(message).Forget();
    }

    private async UniTask ConverAsync(byte[] message)
    {
        await UniTask.SwitchToMainThread();

        byte[] formatted = new byte[message.Length - 1];

        for (int i = 0; i < formatted.Length; i++) 
        {
            formatted[i] = message[i + 1];
        }

        //File.WriteAllBytes(Application.streamingAssetsPath + "/temp.jpeg", formatted);
        //byte[] file = File.ReadAllBytes(Application.streamingAssetsPath + "/temp.jpeg");

        try
        {
            _tempTexture.LoadImage(formatted);
        }
        catch 
        {
            Debug.LogError("Texture error!");
        }
        
        received?.Invoke(_tempTexture);
    }
}