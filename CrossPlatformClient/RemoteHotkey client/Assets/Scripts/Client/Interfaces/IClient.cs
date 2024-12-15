using Cysharp.Threading.Tasks;
using System;

public interface IClient : IDisposable
{
    event Action connected;
    event Action disconnected;
    event Action<string> disconnectedWithError;
    event Action<byte[]> receivedClientDirectedMessage;

    UniTask Connect(string ip, string userName);
    void Disconnect();
    UniTask SendData(byte packegeMarkCode, byte[] message);
}