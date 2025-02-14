using Remote_Hub.Models.PathFinder;
using System.Diagnostics;

namespace Remote_Hub.Models.ModulesBoot
{
    public class ModulesBootModel
    {
        private PathFinderModel _pathFinder;

        public ModulesBootModel()
        {
            _pathFinder = new PathFinderModel();
        }

        public void StartClient()
        {
            StartTempFile(_pathFinder.PathToClient);
        }

        public void StartKeyboardModule()
        {
            StartTempFile(_pathFinder.PathToKeyboardModule);
        }

        private void StartTempFile(string path) => Process.Start(path);
    }
}