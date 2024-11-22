using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

    private int _elapsedBuffer;
    private float _timeElapsed;
    private bool _reconnectLoopStarted;

    private readonly int GRAPHICS_BUFFER;
    private readonly float RESTART_DELLAY;

    private const int BUFFER_SIZE = 999999999;

    public event Action connected;
    public event Action disconnected;
    public event Action<string> disconnectedWithError;
    public event Action<byte[]> receivedClientDirectedMessage;

    private const char USER_NAME_SEPARATE_CHAR = '#';

    public RemoteHotkeyClientModel(int reconnectDelay, int reconnectsCount, int graphicsBuffer, float rESTART_DELLAY)
    {
        GRAPHICS_BUFFER = graphicsBuffer;
        RESTART_DELLAY = rESTART_DELLAY;

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

    public async UniTask Connect(string ip, string userName, bool listening)
    {
        _ip = ip;
        _userName = userName;

        CreateSocket(ip);
        await Connect(_localEndPoint, true);
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
        if (_reconnectLoopStarted == false)
        {
            _reconnectLoopStarted = true;
            ListenerReconnect().Forget();
        }

        await UniTask.SwitchToThreadPool();

        await ConnectListener();

        while (_isConnected == true)
        {
            _timeElapsed = 0f;

            try
            {
                byte[] buffer = new byte[_receivingSocket.ReceiveBufferSize];
                int receivedBufferSize = _receivingSocket.Receive(buffer);
                buffer = ThruncastBuffer(buffer, receivedBufferSize);

                if (buffer[0] == 255)
                {
                    string message = Encoding.ASCII.GetString(buffer);
                    string userName = message.Split(USER_NAME_SEPARATE_CHAR)[0].Trim();

                    _elapsedBuffer += message.Length;

                    if (userName.Trim().Substring(1, userName.Length - 1).Trim() == _userName.Trim())
                    {
                        Debug.Log(buffer.Length);

                        List<byte> splitted = new List<byte>();
                        bool beginned = false;
                        int index = 0;

                        foreach (byte current in buffer)
                        {
                            if (current == USER_NAME_SEPARATE_CHAR && beginned == false)
                            {
                                beginned = true;
                            }

                            if (beginned == true)
                            {
                                splitted.Add(current);
                            }

                            index++;
                        }

                        receivedClientDirectedMessage?.Invoke(splitted.ToArray());
                    }
                }
            }
            catch (Exception e)
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

    private async UniTask ListenerReconnect()
    {
        await UniTask.SwitchToThreadPool();

        float delta = 0.1f;

        while (true)
        {
            _timeElapsed += delta;
            await UniTask.WaitForSeconds(delta);

            if (_elapsedBuffer >= GRAPHICS_BUFFER)
            {
                await ConnectListener();
            }
            else if (_timeElapsed >= RESTART_DELLAY)
            {
                _timeElapsed = 0f;
                _elapsedBuffer = 0;

                Disconnect();
                await Connect(_ip, _userName, true);
            }
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
        _elapsedBuffer = 0;
        await UniTask.SwitchToThreadPool();

        try
        {
            await _receivingSocket.ConnectAsync(_localEndPoint);
        }
        catch
        {
            _receivingSocket.Close();
            CreateListenSocket();
            
            try
            {
                await _receivingSocket.ConnectAsync(_localEndPoint);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
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
                return;
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
        _receivingSocket.ReceiveBufferSize = BUFFER_SIZE;
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