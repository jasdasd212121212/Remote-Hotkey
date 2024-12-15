using RemoteHotkeyProxyMasterServer;
using RemoteHotkeyProxyMasterServer.Server;

Server server = new Server(new Config().IP, 12345);

Console.Read();