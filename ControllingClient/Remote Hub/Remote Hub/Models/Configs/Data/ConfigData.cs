using Newtonsoft.Json;

namespace Remote_Hub.Models.Configs.Data
{
    public class ConfigData
    {
        [JsonProperty] private string _ip;
        [JsonProperty] private string _userName;
        [JsonProperty] private int _compressionLevel;

        [JsonIgnore] public string Ip => _ip;
        [JsonIgnore] public string UserName => _userName;
        [JsonIgnore] public int CompressionLevel => _compressionLevel;

        public ConfigData(string ip, string userName, int compressionLevel) 
        {
            _ip = ip;
            _userName = userName;
            _compressionLevel = compressionLevel;
        }

        public static bool operator ==(ConfigData left, ConfigData right) 
            => left.Ip == right.Ip && left.UserName == right.UserName && left.CompressionLevel == right.CompressionLevel; 

        public static bool operator !=(ConfigData left, ConfigData right) 
            => left.Ip != right.Ip || left.UserName != right.UserName || left.CompressionLevel != right.CompressionLevel;
    }
}