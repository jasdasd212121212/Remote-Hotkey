using RemoteHotkey.Network.Server;
using System.Diagnostics;

namespace RemoteHotkeyCore;

public static class GlobalEntryPoint
{
    private static Thread _inputThread;
    private static IServer _server;
    private static PathFinder _pathFinder;

    private static string _ip = "";
    private static string _userName = "";

    public static void Boot()
    {
        Config config = new Config();
        _pathFinder = new PathFinder();

        Console.WriteLine("Entry -> starting...");

        Thread.Sleep(new Random().Next(1000, 2000));

        _ip = config.Ip;
        _userName = config.Username;

        Start();
    }

    public static void Restart()
    {
        Process.Start(_pathFinder.PathToExe);
        Environment.Exit(0);
    }

    private async static void Start()
    {
        _server = new ProxiedServer(_ip, _userName);

        while (_server.IsConnected == false) { }

        new EntryPoint(_server);

        _inputThread = new Thread(() =>
        {
            while (true)
            {
                Console.WriteLine("You want to disconnect (Y or y) ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    _server.Stop();
                    Console.WriteLine("Client -> dsconnected");

                    return;
                }
            }
        });

        _inputThread.Start();
    }
}