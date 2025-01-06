using Newtonsoft.Json;
using Remote_Hub.Models.Configs.Data;
using Remote_Hub.Models.PathFinder;
using System.IO;

namespace Remote_Hub.Models.Configs
{
    public class Config
    {
        private PathFinderModel _pathFinder;

        public Config() 
        {
            _pathFinder = new PathFinderModel();
        }

        public void WriteConfig(ConfigData data)
        {
            if (ReadConfig() == data)
            {
                return;
            }

            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(_pathFinder.PathToConfig, json);
        }

        public ConfigData ReadConfig()
        {
            string json = File.ReadAllText(_pathFinder.PathToConfig);
            return JsonConvert.DeserializeObject<ConfigData>(json);
        }
    }
}