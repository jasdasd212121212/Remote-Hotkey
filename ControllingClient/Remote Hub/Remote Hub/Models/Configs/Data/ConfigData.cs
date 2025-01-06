using Newtonsoft.Json;

namespace Remote_Hub.Models.Configs.Data
{
    public class ConfigData
    {
        [JsonProperty] private string _ip;
        [JsonProperty] private string _userName;
        [JsonProperty] private int _compressionLevel;

        public string Ip => _ip;
        public string UserName => _userName;
        public int CompressionLevel => _compressionLevel;

        public ConfigData(string ip, string userName, int compressionLevel) 
        {
            _ip = ip;
            _userName = userName;
            _compressionLevel = compressionLevel;
        }
    }
}