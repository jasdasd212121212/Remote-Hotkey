using Newtonsoft.Json;

namespace RemoteHotkeyCore;

public class ConfigData
{
    [JsonProperty] private string _ip;
    [JsonProperty] private string _userName;
    [JsonProperty] private int _compressionLevel;

    public string Ip => _ip;
    public string UserName => _userName;
    public int CompressionLevel => _compressionLevel;
}