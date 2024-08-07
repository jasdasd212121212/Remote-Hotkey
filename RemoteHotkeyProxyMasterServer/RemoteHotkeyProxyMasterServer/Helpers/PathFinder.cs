using System.Reflection;

namespace RemoteHotkeyProxyMasterServer;

public class PathFinder
{
    public string PathToExe => PATH_TO_ASSEMBLY_DLL.Replace(".dll", ".exe");
    public string PathToRoot { get; private set; }

    private readonly string PATH_TO_ASSEMBLY_DLL;

    public PathFinder()
    {
        PATH_TO_ASSEMBLY_DLL = Assembly.GetEntryAssembly().Location;    
        CalculatePathToRoot();
    }

    private void CalculatePathToRoot()
    {
        string[] splittedPath = PATH_TO_ASSEMBLY_DLL.Split("\\");
        PathToRoot = PATH_TO_ASSEMBLY_DLL.Replace(splittedPath[splittedPath.Length - 1], "");
    }
}