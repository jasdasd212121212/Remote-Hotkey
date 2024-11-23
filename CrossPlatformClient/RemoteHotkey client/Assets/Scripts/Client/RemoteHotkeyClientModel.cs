using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RemoteHotkeyClientModel : IDisposable
{
    private Socket _sendSocket;
    private PackegeParserHelper _parserHelper;

    private byte[] _receiveBuffer;

    private string _userName;
    private string _ip;

    private bool _connected;

    private const char USER_NAME_SEPARATE_CHAR = '#';

    public event Action connected;
    public event Action disconnected;
    public event Action<string> disconnectedWithError;
    public event Action<byte[]> receivedClientDirectedMessage;

    public void Dispose()
    {
        Disconnect();   
    }

    public async UniTask Connect(string ip, string userName)
    {
        _sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _parserHelper = new PackegeParserHelper(USER_NAME_SEPARATE_CHAR);

        if (_sendSocket == null || _sendSocket.Connected == true)
        {
            Debug.LogError("Ciritcal error -> can`t connect early connected client");
            return;
        }

        _userName = userName;
        _ip = ip;

        _sendSocket.SendBufferSize = int.MaxValue;
        _sendSocket.ReceiveBufferSize = int.MaxValue;

        await _sendSocket.ConnectAsync(_ip, 12345);

        _connected = true;

        _receiveBuffer = new byte[_sendSocket.ReceiveBufferSize];
        connected?.Invoke();

        ListenLoop().Forget();
    }

    public void Disconnect()
    {
        if (_sendSocket == null || _sendSocket.Connected == false)
        {
            Debug.LogError("Critical error -> can`t disconnect not connected client");
            return;
        }

        _connected = false;

        try
        {
            _sendSocket.Shutdown(SocketShutdown.Both);
            _sendSocket.Dispose();
        }
        catch (Exception e)
        {
            disconnectedWithError?.Invoke(e.Message);
        }

        disconnected?.Invoke();
    }

    public async UniTask SendData(byte packegeMarkCode, byte[] message)
    {
        byte[] userName = Encoding.ASCII.GetBytes(_userName);
        byte[] data = new byte[1] { packegeMarkCode }.Concat(userName).Concat(new byte[1] { (byte)USER_NAME_SEPARATE_CHAR }).Concat(message).ToArray();

        try
        {
            await _sendSocket.SendAsync(data, SocketFlags.None);
        }
        catch (Exception e)
        {
            Debug.LogError($"Critical socket error -> {e.Message}");
        }
    }

    private async UniTask ListenLoop()
    {
        await UniTask.SwitchToThreadPool();

        while (_sendSocket.Connected)
        {
            if (_connected == false)
            {
                await UniTask.SwitchToMainThread();
                return;
            }

            try
            {
                byte[] buffer = ThruncastBuffer(_sendSocket.Receive(_receiveBuffer));

                if (buffer[0] == 255)
                {
                    byte[] message = _parserHelper.GetMessage(buffer);
                    receivedClientDirectedMessage?.Invoke(message);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Critical error -> receive error: {e.Message}");
            }
        }
    }

    private byte[] ThruncastBuffer(int size)
    {
        byte[] newBuffer = new byte[size];

        for (int i = 0; i < size; i++)
        {
            newBuffer[i] = _receiveBuffer[i];
        }

        return newBuffer;
    }
}