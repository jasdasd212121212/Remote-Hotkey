using System;
using System.Collections.Generic;
using System.Linq;

public class ScriptsSaverModel
{
    public event Action<string> scriptLoaded;
    public event Action<string[]> allScriptsLoaded;

    public string[] ScriptsNames { get; private set; }

    public ScriptsSaverModel()
    {
        LoadAllScripts();
    }

    public string LoadScript(string name)
    {
        string result = VirtualStorageFacade.ReadFile(name);

        scriptLoaded?.Invoke(result);

        return result;
    }

    public void SaveScript(string name, string script)
    {
        ScriptsNames = VirtualStorageFacade.GetAllFileNames();

        VirtualStorageFacade.CreateFile(name, script);

        List<string> names = ScriptsNames.ToList();
        names.Add(name);

        ScriptsNames = names.ToArray();

        allScriptsLoaded?.Invoke(ScriptsNames);
    }

    public void Refrash()
    {
        LoadAllScripts();
    }

    private void LoadAllScripts()
    {
        try
        {
            ScriptsNames = VirtualStorageFacade.GetAllFileNames();
            allScriptsLoaded?.Invoke(ScriptsNames);
        }
        catch 
        { 
            ScriptsNames = new string[0]; 
        }
    }
}