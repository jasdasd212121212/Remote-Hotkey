using System.Net;

namespace RemoteHotkey.Network.Server;

public class ServerIPObtainer
{
    public string ObtainIP()
    {
        string hostName = Dns.GetHostName();
        string localMachineIp =
            Dns.GetHostByName(hostName).
            AddressList.
            Where(address => address.ToString().Contains("192")).
            ElementAt(0).
            ToString();

        return localMachineIp;
    }
}