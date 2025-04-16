using Cysharp.Threading.Tasks;
using System;
using System.IO.Compression;
using System.IO;
using UnityEngine;

public class DesktopModel
{
    private IClient _client;
    private Texture2D _tempTexture;

    public event Action<Texture2D> received;

    public DesktopModel(IClient client)
    {
        _client = client;
        _tempTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);

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

        try
        {
            _tempTexture.LoadImage(Decompress(message));
        }
        catch (Exception e)
        {
            Debug.LogError($"Texture error! {e.Message}");
        }

        received?.Invoke(_tempTexture);
    }

    private byte[] Decompress(byte[] compressedString)
    {
        byte[] decompressedBytes;

        var compressedStream = new MemoryStream(compressedString);

        using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
        {
            using (var decompressedStream = new MemoryStream())
            {
                decompressorStream.CopyTo(decompressedStream);

                decompressedBytes = decompressedStream.ToArray();
            }
        }

        return decompressedBytes;
    }
}