using System.Diagnostics;

namespace RemoteHotkeyProxyMasterServer;

public class ProcessRestarter
{
    public void Restart()
    {
        Thread.Sleep(1000);

        Process.Start(new PathFinder().PathToExe);
        Environment.Exit(0);
    }
}