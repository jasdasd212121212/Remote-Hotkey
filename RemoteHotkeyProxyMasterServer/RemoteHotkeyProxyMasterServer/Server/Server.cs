using System.Net;
using System.Net.Sockets;

namespace RemoteHotkeyProxyMasterServer.Server;

public class Server
{
    private Socket _socket;
    private DataSender _sender;

    private const int MIN_PORT = 1024;
    private const int MAX_PORT = 49151;

    public Server(string ip, int port)
    {
        if (port < MIN_PORT || port > MAX_PORT)
        {
            throw new ArgumentException($"Can`t create server on port: {port}. Server can be created with port in disposon from {MIN_PORT} to {MAX_PORT}");
        }

        try
        {
            _sender = new DataSender();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            _socket.Listen(100000);
        }
        catch 
        {
            Console.WriteLine("Server -> ERROR -> socket start");

            Thread.Sleep(500);

            new ProcessRestarter().Restart();
        }

        Listen();
    }

    ~Server() 
    {
        _socket.Close();
        _socket.Dispose();
    }

    private async void Listen()
    {
        Console.WriteLine("Server -> start listening");

        while (true)
        {
            try
            {
                Console.WriteLine("Server -> accept");

                Socket acceptSocket = await _socket.AcceptAsync();
                _sender.AddClient(acceptSocket);

                CreateUser(acceptSocket);

                Console.WriteLine("Server -> listen");
            }
            catch 
            {
                Console.WriteLine("Connection closed -> Restart");
                Restart();
            }
        }
    }

    private void CreateUser(Socket acceptSocket)
    {
        User connectedUser = new User(ref _sender, acceptSocket);

        Thread userThread = new Thread(connectedUser.HandleUser);
        userThread.Start();

        connectedUser.SetThread(userThread);
    }

    private async void Restart()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Server -> ERROR -> restart");
        Console.ForegroundColor = ConsoleColor.White;

        _socket.Close();
        _socket.Dispose();

        await _sender.Broadcast([ServerConstants.RESTART_CONTROLLING_CLIENT_CODE]);

        new ProcessRestarter().Restart();
    }
}