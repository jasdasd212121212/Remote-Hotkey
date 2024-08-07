using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RemoteHotkeyClientModel : IDisposable
{
    private Socket _socket;
    private Socket _receivingSocket;

    private IPEndPoint _localEndPoint;

    private int _reconnectsCount;
    private int _reconnectDelay;

    private string _ip;
    private string _userName;
    private bool _isConnected;

    public event Action connected;
    public event Action disconnected;
    public event Action<string> disconnectedWithError;
    public event Action<byte[]> receivedClientDirectedMessage;

    private const char USER_NAME_SEPARATE_CHAR = '#';

    public RemoteHotkeyClientModel(int reconnectDelay, int reconnectsCount)
    {
        _reconnectDelay = reconnectDelay;
        _reconnectsCount = reconnectsCount;
    }

    public void Dispose()
    {
        try
        {
            Disconnect();
        }
        catch { }
    }

    public async void Connect(string ip, string userName, bool listening)
    {
        _ip = ip;
        _userName = userName;

        CreateSocket(ip);
        await Connect(_localEndPoint, false);
        CreateListenSocket();

        _isConnected = true;

        if (listening == true)
        {
            ListenLoop();
        }
    }

    public async void SendData(byte packegeMarkCode, byte[] content) 
    {
        byte[] codedUserName = Encoding.ASCII.GetBytes(_userName + USER_NAME_SEPARATE_CHAR); 
        byte[] data = new byte[content.Length + 1 + codedUserName.Length];

        data[0] = packegeMarkCode;

        for (int i = 0; i < codedUserName.Length; i++)
        {
            data[i + 1] = codedUserName[i];
        }

        for (int i = 0; i < content.Length; i++)
        {
            data[i + codedUserName.Length + 1] = content[i];
        }

        try
        {
            _socket.Send(data);   
        }
        catch
        {
            await Reconnect();
            SendData(packegeMarkCode, data);
        }
    }

    public async void ListenLoop()
    {
        await UniTask.SwitchToThreadPool();

        await ConnectListener();

        while (_isConnected == true)
        {
            try
            {
                byte[] buffer = new byte[_receivingSocket.ReceiveBufferSize];
                int receivedBufferSize = _receivingSocket.Receive(buffer);
                buffer = ThruncastBuffer(buffer, receivedBufferSize);

                if (buffer[0] == 255)
                {
                    string message = Encoding.ASCII.GetString(buffer);
                    string userName = message.Split(USER_NAME_SEPARATE_CHAR)[0].Trim();

                    if (userName.Trim().Substring(1, userName.Length - 1).Trim() == _userName.Trim())
                    {
                        receivedClientDirectedMessage?.Invoke(Encoding.ASCII.GetBytes(message.Split(USER_NAME_SEPARATE_CHAR)[1]));

                        Debug.Log(message);
                    }
                }
            }
            catch 
            {
                await ConnectListener();
            }
        }
    }

    private void Disconnect()
    {
        disconnected?.Invoke();

        if (_isConnected == true)
        {
            _isConnected = false;

            _socket.Shutdown(SocketShutdown.Both);
            _receivingSocket.Shutdown(SocketShutdown.Both);

            _socket.Dispose();
            _receivingSocket.Dispose();

            Debug.Log("Disconnecting...");
        }
    }

    private async UniTask Reconnect()
    {
        for (int i = 0; i < _reconnectsCount; i++)
        {
            if (await HandleDisconnect() == true)
            {
                break;
            }
            else
            {
                await UniTask.Delay(_reconnectDelay);
            }
        }
    }

    private async UniTask ConnectListener()
    {
        await UniTask.SwitchToThreadPool();

        try
        {
            await _receivingSocket.ConnectAsync(_localEndPoint);
        }
        catch
        {
            _receivingSocket.Close();
            CreateListenSocket();

            await _receivingSocket.ConnectAsync(_localEndPoint);
        }
    }

    private async UniTask Connect(IPEndPoint localEndPoint, bool generateException)
    {
        try
        {
            await _socket.ConnectAsync(localEndPoint);
            connected?.Invoke();

            Debug.Log("Client sucesfully connected");
        }
        catch (Exception e)
        {
            if (generateException == true)
            {
                throw new NotImplementedException(e.Message);
            }
            else
            {
                Debug.LogError($"Connecting error: {e}");
                disconnectedWithError?.Invoke(e.Message);
            }
        }
    }

    private void CreateSocket(string ip)
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _localEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);
    }

    private void CreateListenSocket()
    {
        _receivingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    private byte[] ThruncastBuffer(byte[] source, int targetBufferSize)
    {
        byte[] result = new byte[targetBufferSize];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = source[i];
        }

        return result;
    }

    private async UniTask<bool> HandleDisconnect()
    {
        try
        {
            _socket.Close();
            _socket.Dispose();

            CreateSocket(_ip);
            await Connect(_localEndPoint, true);

            return true;
        }
        catch
        {
            disconnected?.Invoke();
            return false;
        }
    }
}