using System.Net.Sockets;
using System.Text;

namespace RemoteHotkeyProxyMasterServer.Server;

public class User
{
    private DataSender _sender;
    private Socket _socket;
    private Thread _thread;

    private bool _connected;

    public User(ref DataSender sender, Socket socket) 
    {
        _sender = sender;
        _socket = socket;

        _socket.ReceiveBufferSize = 999999999;
    }

    public void SetThread(Thread thread)
    {
        if (_thread != null)
        {
            return;
        }

        _thread = thread;
    }

    public async void HandleUser()
    {
        _connected = true;
        Console.WriteLine("User -> connected");

        while (_connected == true)
        {
            try
            {
                byte[] buffer = new byte[_socket.ReceiveBufferSize];
                int receivedBytesCount = await _socket.ReceiveAsync(buffer);
                buffer = ThruncastBytes(buffer, receivedBytesCount);

                string debugMessage = buffer.Length < 1000 ? Encoding.ASCII.GetString(buffer) : $"Bufer too long. Length: {buffer.Length}";

                Console.WriteLine($"Server -> send {debugMessage}");

                if (buffer.Length == 0)
                {
                    Console.WriteLine("User -> empty buffer");
                    Disconnect();

                    break;
                }
                else
                {
                    await _sender.Broadcast(buffer, _socket);
                }
            }
            catch (Exception e) 
            {
                Disconnect();
            }
        }
    }

    private byte[] ThruncastBytes(byte[] source, int bytesCount)
    {
        byte[] newBuffer = new byte[bytesCount];

        for (int i = 0; i < newBuffer.Length; i++)
        {
            newBuffer[i] = source[i];
        }

        return newBuffer;
    }

    private void Disconnect()
    {
        _connected = false;

        try
        {
            _socket.Close();
            _socket.Dispose();

            _sender.RemoveClient(_socket);
        }
        catch (Exception e)
        { 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Catched error -> {e.Message}; StackTrace: {e.StackTrace}");
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                _socket.Close();
                _socket.Dispose();
            }
            catch (Exception socketE)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Catched SOCKET error WHEN DISCONNECTING -> {socketE.Message}; StackTrace: {socketE.StackTrace}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        Console.WriteLine("User -> disconnected");
    }
}