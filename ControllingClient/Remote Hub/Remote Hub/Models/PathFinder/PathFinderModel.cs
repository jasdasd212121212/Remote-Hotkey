using System;
using System.Reflection;

namespace Remote_Hub.Models.PathFinder
{
    public class PathFinderModel
    {
        private string _pathToClient;
        private string _pathToConfig;
        private string _pathToKeyboardModule;

        public string PathToClient => _pathToClient;
        public string PathToConfig => _pathToConfig;
        public string PathToKeyboardModule => _pathToKeyboardModule;

        public string ClientLocationDisk => DISK;

        private readonly string PATH_TO_ASSEMBLY_DLL;
        private readonly string SELF_NAME;
        private readonly string DISK;

        private const string CLIENT_NAME = "RemoteHotkey";
        private const string CONFIG_NAME = "Config";
        private const string KEYBOARD_MODULE_NAME = "WPF_KeyboardCapturer";

        public PathFinderModel()
        {
            PATH_TO_ASSEMBLY_DLL = Assembly.GetEntryAssembly().Location;

            string[] splittedPath = GetSplittedPathToAssembly();

            SELF_NAME = splittedPath[splittedPath.Length - 1].Replace(".exe", "");
            DISK = $"{splittedPath[0][0]}:";

            CalculatePathToClient();
            CalculatePathToConfig();
            CalculatePathToKeyboardModule();
        }

        private void CalculatePathToClient()
            => CalculatePathToTarget(ref _pathToClient, CLIENT_NAME, CLIENT_NAME, "exe", true);

        private void CalculatePathToConfig()
            => CalculatePathToTarget(ref _pathToConfig, CLIENT_NAME, CONFIG_NAME, "json", true);

        private void CalculatePathToKeyboardModule() 
            => CalculatePathToTarget(ref _pathToKeyboardModule, KEYBOARD_MODULE_NAME, KEYBOARD_MODULE_NAME, "exe", false);

        private void CalculatePathToTarget(ref string result, string moduleName, string targetName, string extension, bool useDotNet)
        {
            string rootFolderPath = SplitPath(GetSplittedPathToAssembly(), SELF_NAME, 2);
            result = $"{rootFolderPath}\\{moduleName}\\{moduleName}\\bin\\Debug{(useDotNet ? "\\net8.0" : "")}\\{targetName}.{extension}";
        }

        private string[] GetSplittedPathToAssembly() => PATH_TO_ASSEMBLY_DLL.Split(new string[] { "\\" }, StringSplitOptions.None);

        private string SplitPath(string[] source, string separator, int targetSeparatorsCount)
        {
            string result = "";
            int separatorsCount = 0;
            int endIndex = 0;

            int conut = source.Length - 1;

            for (int i = conut; i >= 0; i--)
            {
                if (source[i].Trim() == separator.Trim())
                {
                    separatorsCount++;
                }

                if (separatorsCount >= targetSeparatorsCount)
                {
                    endIndex = i;
                    break;
                }
            }

            for (int i = 0; i < endIndex; i++)
            {
                result += $"{(i > 0 ? "\\" : "")}{source[i]}";
            }

            return result;
        }
    }
}