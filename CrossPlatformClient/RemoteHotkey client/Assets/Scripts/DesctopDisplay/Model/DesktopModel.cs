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

        _tempTexture.LoadRawTextureData(message);
        received?.Invoke(_tempTexture);
    }
}