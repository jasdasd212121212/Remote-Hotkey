namespace RemoteHotkeyProxyMasterServer;

public class Config
{
    public string IP => File.ReadAllText($"{new PathFinder().PathToRoot}/Config.txt");
}