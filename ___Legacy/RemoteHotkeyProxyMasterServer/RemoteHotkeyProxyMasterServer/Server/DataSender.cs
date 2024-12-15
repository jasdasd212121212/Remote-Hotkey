using System.Net.Sockets;

namespace RemoteHotkeyProxyMasterServer.Server;

public class DataSender
{
    private List<Socket> _clients = new List<Socket>();

    public async Task Broadcast(byte[] data, Socket origin)
    {
        try
        {
            foreach (Socket client in _clients)
            {
                if (client != origin)
                {
                    if (client.Connected == true)
                    {
                        await client.SendAsync(data);
                    }
                }
            }
    }
        catch { }
    }

    public async Task Broadcast(byte[] data)
    {
        try
        {
            foreach (Socket client in _clients)
            {
                if (client.Connected)
                {
                    await client.SendAsync(data);
                }
            }
        }
        catch { }
    }

    public void RemoveClient(Socket client)
    {
        if (_clients.Contains(client))
        {
            _clients.Remove(client);
        }
    }

    public void AddClient(Socket client)
    {
        if (_clients.Contains(client) == false)
        {
            _clients.Add(client);
        }
    }
}