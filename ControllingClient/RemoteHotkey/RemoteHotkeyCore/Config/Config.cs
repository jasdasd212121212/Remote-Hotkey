using Newtonsoft.Json;

namespace RemoteHotkeyCore;

public class Config
{
    public ConfigData Data { get; private set; }

    public Config()
    {
        PathFinder pathFinder = new PathFinder();

        string json = File.ReadAllText($"{pathFinder.PathToRoot}\\Config.json");
        Data = JsonConvert.DeserializeObject<ConfigData>(json);
    }
}