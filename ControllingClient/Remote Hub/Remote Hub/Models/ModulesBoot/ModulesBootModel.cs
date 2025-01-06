using Remote_Hub.Models.PathFinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Remote_Hub.Models.ModulesBoot
{
    public class ModulesBootModel
    {
        private PathFinderModel _pathFinder;

        private List<string> _prefixes = new List<string>();

        private const string TEMP_FILE_NAME = "Temp.bat";

        private string CurrentFile => GetPrefixesName(_prefixes.Count - 1);

        public ModulesBootModel()
        {
            _pathFinder = new PathFinderModel();
        }

        ~ModulesBootModel()
        {
            for (int i = 0; i < _prefixes.Count; i++)
            {
                File.Delete(GetPrefixesName(i));
            }

            _prefixes.Clear();
        }

        public void StartClient()
        {
            WriteTempFile(_pathFinder.PathToClient);
            StartTempFile();
        }

        public void StartKeyboardModule()
        {
            WriteTempFile(_pathFinder.PathToKeyboardModule);
            StartTempFile();
        }

        private void WriteTempFile(string path)
        {
            AddNewPrefix();
            File.WriteAllText(CurrentFile, $"@echo off\n{_pathFinder.ClientLocationDisk}\nstart {path}");
        }

        private void StartTempFile() => Process.Start(CurrentFile);
        private void AddNewPrefix() => _prefixes.Add(Guid.NewGuid().ToString() + "-");
        private string GetPrefixesName(int preffixIndex) => _prefixes[preffixIndex] + TEMP_FILE_NAME;
    }
}